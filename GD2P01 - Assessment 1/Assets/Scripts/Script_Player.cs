using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    #region Member Variables
    [SerializeField] bool m_Tutorial = false;
    [SerializeField] float m_MoveSpeed = 5.0f, m_DodgeSpeed = 5.0f, m_DodgeLength_s = 0.5f, m_TurnSpeed = 5.0f, m_InteractDistance = 5.0f, m_DamageInterval_s = 0.2f,
        m_HeadCheckDistance = 1.0f, m_MaxHealth = 100.0f;
    [SerializeField] Script_Gun m_ActiveWeapon;
    [SerializeField] AudioClip m_RollSound, m_HitSound;

    AudioSource m_AudioSource;
    GameObject m_Mesh;
    CharacterController m_Controller;
    RaycastHit m_MouseHit, m_HeadCheckHit, m_InteractHit;
    Vector3 m_MousePos3D;
    Vector2 m_InputVector = Vector2.zero;
    bool m_Crouched = false, m_Rolling = false, m_Interacting = false, m_TakingDamage = false;
    float m_TurnSmoothVelocity = 0, m_RollTimer = 0, m_Health = 0;
    #endregion

    #region Public
    public float GetHealth()
    {
        return m_Health;
    }
    public float GetMaxHealth()
    {
        return m_MaxHealth;
    }
    #endregion

    #region Private
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Mesh = GetComponentInChildren<MeshFilter>().gameObject;
        m_AudioSource = GetComponent<AudioSource>();

        m_Health = m_MaxHealth;
    }
    void Update()
    {
        ApplyGravity();

        if (!m_Interacting)
        {
            HandleLookAtMouse();

            ReturnInput();
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out m_HeadCheckHit, m_HeadCheckDistance))
            {
                if (m_HeadCheckHit.transform.gameObject != null)
                {
                    m_Crouched = true;
                }
            }
            HandleRoll();

            if (!m_Rolling)
            {
                if (m_Crouched)
                {
                    SetGameObjectCrouched();

                    HandleMovement(m_MoveSpeed / 2);
                }
                else
                {
                    SetGameObjectStanding();

                    HandleMovement(m_MoveSpeed);
                }
            }
        }

        CheckForDeath();
    }
    void SetGameObjectCrouched()
    {
        if (m_Mesh.transform.localScale == Vector3.one)
        {
            m_Mesh.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
            m_Controller.height = 0.5f;
        }
    }
    void SetGameObjectStanding()
    {
        if(m_Mesh.transform.localScale != Vector3.one && !m_Crouched)
        {
            m_Mesh.transform.localScale = Vector3.one;
            m_Controller.height = 2.0f;
        }
    }
    void HandleMovement(float _moveSpeed)
    {
        if (m_InputVector.magnitude > 0.1f)
            m_Controller.Move(new Vector3(m_InputVector.x, 0.0f, m_InputVector.y) * _moveSpeed * Time.deltaTime);
    }
    void HandleLookAtMouse()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out m_MouseHit))
        {
            m_MousePos3D = m_MouseHit.point;
        }

        float targetAngle;
        targetAngle = Mathf.Atan2((transform.position.x - m_MousePos3D.x), transform.position.z - m_MousePos3D.z) * Mathf.Rad2Deg + 180;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, m_TurnSpeed);
        Vector3 movedir = Quaternion.Euler(0.0f, smoothedAngle, 0.0f) * Vector3.forward;
        movedir.Normalize();
        transform.rotation = Quaternion.Euler(0.0f, smoothedAngle, 0.0f);
    }
    void HandleRoll()
    {
        if (m_RollTimer > 0)
            m_RollTimer -= Time.deltaTime;

        if (m_Rolling)
        {
            m_Controller.Move(transform.rotation * Vector3.forward * m_DodgeSpeed * Time.deltaTime);
        }
    }
    void ApplyGravity()
    {
        m_Controller.Move(Vector3.down* 9.81f * Time.deltaTime);
    }
    void CheckForDeath()
    {
        if (m_Health <= 0.0f)
        {
            if (m_Tutorial)
                GameObject.FindObjectOfType<Script_WinCondition>().SwitchToTutorial();
            else
                GameObject.FindObjectOfType<Script_WinCondition>().SwitchToMissionFailed();
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Bullet")
        {
            Script_Bullet bulletScript = _other.transform.GetComponent<Script_Bullet>();
            if (!bulletScript.IsFriendly())
            {
                if (!m_TakingDamage)
                    StartCoroutine(DamageRoutine(bulletScript.GetDamage()));

            }
        }
    }
    void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.tag == "Bullet")
        {
            Script_Bullet bulletScript = _other.transform.GetComponent<Script_Bullet>();
            if (!bulletScript.IsFriendly())
            {
                if (!m_TakingDamage)
                    StartCoroutine(DamageRoutine(bulletScript.GetDamage()));

            }
        }
    }
    Vector2 ReturnInput()
    {
        m_InputVector = Vector2.zero;
        m_Crouched = false;

        if (Input.GetKey(KeyCode.W))
        {
            m_InputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_InputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_InputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_InputVector.x = 1;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            m_Crouched = true;
            SetGameObjectCrouched();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!m_Rolling)
                StartCoroutine(RollRoutine());
        }
        if (Input.GetMouseButton(0))
        {
            m_ActiveWeapon.Fire();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (!m_Interacting)
                StartCoroutine(InteractRoutine());
        }

        return m_InputVector.normalized;
    }
    IEnumerator InteractRoutine()
    {
        m_Interacting = true;
        Script_Interactable hitObject = null;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out m_InteractHit, m_InteractDistance))
        {
            hitObject = m_InteractHit.transform.GetComponent<Script_Interactable>();
            
        }
        if (hitObject != null)
        {
            hitObject.Interact();
            yield return new WaitUntil(() => hitObject.HasInteractFinished());
        }
        else
            yield return null;

        m_Interacting = false;
    }
    IEnumerator RollRoutine()
    {
        m_AudioSource.PlayOneShot(m_RollSound);
        m_Rolling = true;
        m_RollTimer = m_DodgeLength_s;
        SetGameObjectCrouched();
        yield return new WaitUntil(() => m_RollTimer <= 0);
        SetGameObjectStanding();
        m_Rolling = false;
    }
    IEnumerator DamageRoutine(float _amount)
    {
        m_AudioSource.PlayOneShot(m_HitSound);
        m_TakingDamage = true;
        for (int i = 0; i < _amount; _amount--)
        {
            if (m_Health > 0)
            {
                m_Health--;
            }
            else
            {
                yield return null;
            }
        }
        yield return new WaitForSeconds(m_DamageInterval_s);
        m_TakingDamage = false;
    }
    #endregion
}


