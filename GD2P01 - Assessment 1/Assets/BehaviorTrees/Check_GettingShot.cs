using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_GettingShot : BehaviorNode
{
    #region Member Variables
    Script_Enemy m_EnemyScript;
    #endregion

    #region Public
    public Check_GettingShot(Script_Enemy _enemy)
    {
        m_EnemyScript = _enemy;
    }
    public override BehaviorNodeState Evaluate()
    {
        if (m_EnemyScript.IsTakingDamage())
        {
            m_EnemyScript.GetManager().SetLastKnownLocation(m_EnemyScript.GetPlayerCache().position);
            m_EnemyScript.SetAlertMax();
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
