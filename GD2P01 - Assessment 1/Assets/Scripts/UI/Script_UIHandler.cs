// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_UIHandler.cs 
// Description : Handles Opening And Closing The Pause Menu And Disabling the Player Script
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_UIHandler : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] GameObject m_PauseMenu;
    Script_Player m_PlayerScript;
    #endregion

    #region Private
    void Start()
    {
        // Grab Player Script
        m_PlayerScript = GameObject.FindObjectOfType<Script_Player>();
    }
    void Update()
    {
        // If TAB is pressed, open pause menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_PauseMenu.SetActive(!m_PauseMenu.activeSelf);
        }
        // If pause menu open, disable player script else enable it
        if (m_PauseMenu.activeSelf)
        {
            m_PlayerScript.enabled = false;
        }
        else
        {
            m_PlayerScript.enabled = true;
        }
    }
    #endregion
}
