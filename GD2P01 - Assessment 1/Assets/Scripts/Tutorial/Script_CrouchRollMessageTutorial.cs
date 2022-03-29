using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CrouchRollMessageTutorial : MonoBehaviour
{
    Script_DialoguePopup m_DialoguePopup;
    bool m_Triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        m_DialoguePopup = GameObject.FindObjectOfType<Script_DialoguePopup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" && !m_Triggered)
        {
            m_DialoguePopup.DetectionMessageTutorial();
            m_Triggered = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.tag == "Player" && !m_Triggered)
        {
            m_DialoguePopup.DetectionMessageTutorial();
            m_Triggered = true;
        }
    }
}
