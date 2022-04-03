using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_HPLow : BehaviorNode
{
    #region Member Variables
    Script_Enemy m_EnemyScript;
    #endregion

    #region Public
    public Check_HPLow(Script_Enemy _enemyScript)
    {
        m_EnemyScript = _enemyScript;

    }
    public override BehaviorNodeState Evaluate()
    {

        if (m_EnemyScript.IsHealthLow())
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
