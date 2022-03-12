using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bullet : MonoBehaviour
{
    #region Member Variables
    [SerializeField] float m_Damage = 10.0f;
    [SerializeField] float m_TravelSpeed = 5.0f;
    [SerializeField] bool m_Friendly = true;

    Vector3 m_Direction;
    #endregion

    #region Public
    public void SetDirection(Vector3 _direction)
    {
        m_Direction = _direction;
        transform.rotation = Quaternion.LookRotation(Vector3.up, _direction);
    }
    public float GetDamage()
    {
        return m_Damage;
    }
    public bool IsFriendly()
    {
        return m_Friendly;
    }
    #endregion

    #region Private
    void Update()
    {
        transform.position += m_Direction * m_TravelSpeed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider _other)
    {
        if (m_Friendly)
        {
            if (_other.gameObject.tag != "Player" && _other.gameObject.tag != "Bullet")
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (_other.gameObject.tag == "Player" && _other.gameObject.tag != "Bullet")
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnTriggerStay(Collider _other)
    {
        if (m_Friendly)
        {
            if (_other.gameObject.tag != "Player" && _other.gameObject.tag != "Bullet")
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (_other.gameObject.tag == "Player" && _other.gameObject.tag != "Bullet")
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion
}
