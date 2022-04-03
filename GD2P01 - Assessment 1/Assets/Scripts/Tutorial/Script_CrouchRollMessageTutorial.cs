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
        // Find and Assign Dialogue Popup script
        m_DialoguePopup = GameObject.FindObjectOfType<Script_DialoguePopup>();
    }
    void OnTriggerEnter(Collider other)
    {
        // If player Enter's trigger for first time
        if (other.transform.root.tag == "Player" && !m_Triggered)
        {
            m_DialoguePopup.DetectionMessageTutorial();
            m_Triggered = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        // If player Enter's trigger for first time and on trigger enter missed it
        if (other.transform.root.tag == "Player" && !m_Triggered)
        {
            m_DialoguePopup.DetectionMessageTutorial();
            m_Triggered = true;
        }
    }
    #endregion
}
