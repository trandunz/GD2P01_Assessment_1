using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_TargetAlive : BehaviorNode
{
    #region Member Variables
    Transform m_Target;
    #endregion

    #region Public
    public Check_TargetAlive(Transform _target)
    {
        m_Target = _target;
    }
    public override BehaviorNodeState Evaluate()
    {
        if (m_Target)
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
