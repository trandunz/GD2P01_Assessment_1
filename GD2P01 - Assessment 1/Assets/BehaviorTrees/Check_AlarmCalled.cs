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
