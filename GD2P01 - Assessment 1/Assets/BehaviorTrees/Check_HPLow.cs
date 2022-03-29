using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_HPLow : BehaviorNode
{
    #region Member Variables
    Script_Enemy m_Entity;
    #endregion

    #region Public
    public Check_HPLow(Script_Enemy _entity)
    {
        m_Entity = _entity;

    }
    public override BehaviorNodeState Evaluate()
    {

        if (m_Entity.HealthLow())
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
