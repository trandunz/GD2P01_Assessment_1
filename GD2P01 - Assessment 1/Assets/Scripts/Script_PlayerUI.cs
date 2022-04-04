// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_PlayerUI.cs 
// Description : Player UI Implemention File
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_PlayerUI : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] Image m_HealthBar;
    Script_Player m_PlayerScript;
    #endregion

    #region Private
    void Start()
    {
        // Assign and grab player script
        m_PlayerScript = GetComponent<Script_Player>();
    }
    void Update()
    {
        // Constantly updates Health bar UI based on current health with respect to the players maxHealth
        m_HealthBar.fillAmount = m_PlayerScript.GetHealth() / m_PlayerScript.GetMaxHealth();
    }
    #endregion
}
