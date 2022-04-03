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
        // If bullet of any kind hits window then play the sound and break the window.
        if (other.gameObject.tag is "Bullet")
        {
            Destroy(Instantiate(m_GlassBreakSoundPrefab, transform.position, Quaternion.identity), 2.0f);
            Destroy(this.gameObject);
            
        }
    }
    void OnTriggerStay(Collider other)
    {
        // Calleed if enter failed to get called.
        // If bullet of any kind hits window then play the sound and break the window.
        if (other.gameObject.tag is "Bullet")
        {
            Destroy(Instantiate(m_GlassBreakSoundPrefab, transform.position, Quaternion.identity), 2.0f);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
