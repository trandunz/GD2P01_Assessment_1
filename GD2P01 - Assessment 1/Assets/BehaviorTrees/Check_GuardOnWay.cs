// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Check_GuardOnWay.cs 
// Description : Checks If A Guard Is On The Way To The Alarm
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_GuardOnWay : BehaviorNode
{
    #region Member Variables
    Script_Alarm m_Alarm;
    Script_Enemy m_Enemy;
    #endregion

    #region Public
    public Check_GuardOnWay(Script_Alarm _alarm,  Script_Enemy _enemyScript)
    {
        m_Enemy = _enemyScript;
        m_Alarm = _alarm;
    }
    public override BehaviorNodeState Evaluate()
    {
        if (!m_Alarm.IsGuardOnWay())
        {
            m_Alarm.SetGuardOnWay(m_Enemy);
            m_Enemy.m_OnRoute = true;
        }

        if (m_Enemy.m_OnRoute)
        {
            p_State = BehaviorNodeState.FAILURE;
        }
        else if (m_Alarm.IsGuardOnWay())
        {
            p_State = BehaviorNodeState.SUCCESS;
        }
        else
        {
            p_State = BehaviorNodeState.FAILURE;
        }

        return p_State;
    }
    #endregion
}
