using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Window : MonoBehaviour
{
    [SerializeField] GameObject m_GlassBreakSoundPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Bullet")
        {
            Destroy(Instantiate(m_GlassBreakSoundPrefab, transform.position, Quaternion.identity), 2.0f);
            Destroy(this.gameObject);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag is "Bullet")
        {
            Destroy(Instantiate(m_GlassBreakSoundPrefab, transform.position, Quaternion.identity), 2.0f);
            Destroy(this.gameObject);
        }
    }
}
