// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_BossMessageTutorial.cs 
// Description : Displays The Boss Tutorial Message On First Trigger Detection
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_BossMessageTutorial : MonoBehaviour
{
    #region MemberVariables
    Script_DialoguePopup m_DialoguePopup;
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
            m_DialoguePopup.BossMessageTutorial();
            m_Triggered = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        // If player Enter's trigger for first time and on trigger enter missed it
        if (other.transform.root.tag == "Player" && !m_Triggered)
        {
            m_DialoguePopup.BossMessageTutorial();
            m_Triggered = true;
        }
    }
    #endregion
}

