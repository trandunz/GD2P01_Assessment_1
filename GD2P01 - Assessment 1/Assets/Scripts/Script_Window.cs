using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Window : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] GameObject m_GlassBreakSoundPrefab;
    #endregion

    #region Private
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Bullet")
        {
            Destroy(Instantiate(m_GlassBreakSoundPrefab, transform.position, Quaternion.identity), 2.0f);
            Destroy(this.gameObject);
            
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag is "Bullet")
        {
            Destroy(Instantiate(m_GlassBreakSoundPrefab, transform.position, Quaternion.identity), 2.0f);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
