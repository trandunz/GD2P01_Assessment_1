// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_Alarm.cs 
// Description : Alarm Implementation File
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Alarm : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] bool m_Activated = false;
    Script_Reinforcements Script_ReinforcementVan;
    Script_Enemy m_GuardOnWay = null;
    Script_DialoguePopup m_DialoguePopupHandler;
    #endregion

    #region Private
    void Start()
    {
        // Grab diologue popuup script
        m_DialoguePopupHandler = GameObject.FindWithTag("DialoguePopup").GetComponent<Script_DialoguePopup>();

        // Grab reinforcement van script
        Script_ReinforcementVan = GameObject.FindWithTag("Reinforcements").GetComponent<Script_Reinforcements>();
    }
    /// <summary>
    /// Wait 5 seconds and start van pull up
    /// </summary>
    /// <returns></returns>
    IEnumerator CallReinforcementsRoutine()
    {
        yield return new WaitForSeconds(5);
        Script_ReinforcementVan.PullUp();
    }
    #endregion

    #region Public
    public bool IsActivated()
    {
        return m_Activated;
    }
    public bool IsGuardOnWay()
    {
        return m_GuardOnWay;
    }
    /// <summary>
    /// Triggers the alarm and calls reinforcements
    /// </summary>
    public void Trigger()
    {
        StartCoroutine(CallReinforcementsRoutine());
        m_Activated = true;
        GetComponent<AudioSource>().Play();
        m_DialoguePopupHandler.SwatTeamMessage();
    }
    /// <summary>
    /// Sets the specified guard on way to trigger alarm
    /// </summary>
    /// <param name="_guard"></param>
    public void SetGuardOnWay(Script_Enemy _guard)
    {
        m_GuardOnWay = _guard;
    }
    #endregion
}
