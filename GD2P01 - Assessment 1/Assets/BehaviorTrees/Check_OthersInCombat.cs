using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_OthersInCombat : BehaviorNode
{
    #region Member Variables
    Script_EnemyManager m_EnemyManager;
    #endregion

    #region Public
    public Check_OthersInCombat(Script_EnemyManager _enemyManager)
    {
        m_EnemyManager = _enemyManager;
    }
    public override BehaviorNodeState Evaluate()
    {
        if (m_EnemyManager.IsEnemyInCombat())
        {
            Debug.Log("Others In Combat!");
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
