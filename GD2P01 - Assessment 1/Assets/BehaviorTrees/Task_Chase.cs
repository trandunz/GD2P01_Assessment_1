using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Task_Chase : BehaviorNode
{
    #region Member Variables
    NavMeshAgent m_Agent;
    Transform m_Target;
    #endregion

    #region Public
    public Task_Chase(NavMeshAgent _agent, Transform _target)
    {
        m_Agent = _agent;
        m_Target = _target;
    }
    public override BehaviorNodeState Evaluate()
    {

        m_Agent.SetDestination(m_Target.position);
        if (m_Agent)
        {
            m_Agent.isStopped = false;
        }
        m_Agent.transform.position = m_Agent.nextPosition;

        p_State = BehaviorNodeState.RUNNING;
        return p_State;
    }
    #endregion
}
