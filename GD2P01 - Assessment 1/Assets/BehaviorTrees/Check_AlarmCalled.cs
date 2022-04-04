// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Check_AlarmCalled.cs 
// Description : Checks If The Alarm Is Called
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_AlarmCalled : BehaviorNode
{
    #region Member Variables
    Script_Alarm m_Alarm;
    #endregion

    #region Public
    public Check_AlarmCalled(Script_Alarm _alarm)
    {
        m_Alarm = _alarm;
    }
    public override BehaviorNodeState Evaluate()
    {
        if (m_Alarm.IsActivated())
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
