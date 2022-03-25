using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_TargetAlive : BehaviorNode
{
    #region Member Variables
    Transform m_Target;
    Script_Enemy m_EnemyScript;
    #endregion

    #region Public
    public Check_TargetAlive(Transform _target, Script_Enemy _enemyScript)
    {
        m_Target = _target;
        m_EnemyScript = _enemyScript;
    }
    public override BehaviorNodeState Evaluate()
    {
        if (m_Target)
        {
            p_State = BehaviorNodeState.SUCCESS;
        }
        else
        {
            m_EnemyScript.m_InCombat = false;
            p_State = BehaviorNodeState.FAILURE;
        }

        return p_State;
    }
    #endregion
}
