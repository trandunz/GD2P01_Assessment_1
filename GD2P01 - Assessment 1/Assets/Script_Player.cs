using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    Vector2 m_InputVector = Vector2.zero;
    bool m_Crouched = false;
    bool m_Rolling = false;

    float m_TurnSmoothVelocity = 0;
    float m_RollTimer = 0;

    [SerializeField] float m_MoveSpeed = 5.0f;
    [SerializeField] float m_DodgeSpeed = 5.0f;
    [SerializeField] float m_DodgeLength_s = 0.5f;
    [SerializeField] float m_TurnSpeed = 5.0f;

    [SerializeField] Script_Gun m_ActiveWeapon;
    GameObject m_Mesh;
    CharacterController m_Controller;
    RaycastHit m_Hit;
    Vector3 m_MousePos3D;

    private void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Mesh = GetComponentInChildren<MeshFilter>().gameObject;
    }

    private void Update()
    {
        HandleLookAtMouse();
        ApplyGravity();
        ReturnInput();
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
        if (m_Mesh.transform.localScale != Vector3.one && !m_Crouched)
        {
            m_Mesh.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out m_Hit))
        {
            m_MousePos3D = m_Hit.point;
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


    IEnumerator RollRoutine()
    {
        m_Rolling = true;
        m_RollTimer = m_DodgeLength_s;
        SetGameObjectCrouched();
        yield return new WaitUntil(() => m_RollTimer <= 0);
        SetGameObjectStanding();
        m_Rolling = false;
    }


    void ApplyGravity()
    {
        m_Controller.Move(Vector3.down* 9.81f * Time.deltaTime);
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

        return m_InputVector.normalized;
    }
}

