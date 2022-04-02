using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CrouchRollMessageTutorial : MonoBehaviour
{
    #region MemberVariables
    Script_DialoguePopup m_DialoguePopup = null;
    bool m_Triggered = false;
    #endregion

    #region Private
    void Start()
    {
        m_DialoguePopup = GameObject.FindObjectOfType<Script_DialoguePopup>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" && !m_Triggered)
        {
            m_DialoguePopup.DetectionMessageTutorial();
            m_Triggered = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.root.tag == "Player" && !m_Triggered)
        {
            m_DialoguePopup.DetectionMessageTutorial();
            m_Triggered = true;
        }
    }
    #endregion
}
