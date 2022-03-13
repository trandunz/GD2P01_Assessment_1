using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task_MoveToTarget : BehaviorNode
{
    #region Member Variables
    NavMeshAgent m_Agent;
    Script_EnemyManager m_EnemyManager;
    #endregion

    #region Public
    public Task_MoveToTarget(NavMeshAgent _agent, Script_EnemyManager _enemyManager)
    {
        m_Agent = _agent;
        m_EnemyManager = _enemyManager;
    }
    public override BehaviorNodeState Evaluate()
    { 
        if (m_Agent)
        {
            m_Agent.SetDestination(m_EnemyManager.GetLastKnownLocation());
            m_Agent.isStopped = false;
        }
        
        p_State = BehaviorNodeState.RUNNING;

        return p_State;
    }
    #endregion
}
