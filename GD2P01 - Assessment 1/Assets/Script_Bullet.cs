using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bullet : MonoBehaviour
{
    [SerializeField] float m_TravelSpeed = 5.0f;
    Vector3 m_Direction;
    // Update is called once per frame
    void Update()
    {
        transform.position += m_Direction * m_TravelSpeed * Time.deltaTime;
    }

    public void SetDirection(Vector3 _direction)
    {
        m_Direction = _direction;
        transform.rotation = Quaternion.LookRotation(Vector3.up, _direction);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
