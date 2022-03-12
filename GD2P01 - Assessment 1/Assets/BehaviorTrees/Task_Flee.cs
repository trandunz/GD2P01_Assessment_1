using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task_Flee : BehaviorNode
{
    #region Member Variables
    NavMeshAgent m_Agent;
    Transform m_Target;
    Transform m_StartingWaypoint;
    #endregion

    #region Public
    public Task_Flee(NavMeshAgent _agent, Transform _startingWaypoint, Transform _target)
    {
        m_Agent = _agent;
        m_Target = _target;
        m_StartingWaypoint = _startingWaypoint;
    }
    public override BehaviorNodeState Evaluate()
    {
        // BROKEN
        if ((m_Target.position - m_Agent.transform.position).magnitude <= 10.0f)
        {
            m_Agent.transform.LookAt(new Vector3(m_Target.position.x, m_Agent.transform.position.y, m_Target.position.z));
            m_Agent.isStopped = true;
            m_Agent.GetComponent<Script_Enemy>().m_OnRoute = false;
            p_State = BehaviorNodeState.FAILURE;
        }
        else
        {
            m_Agent.GetComponent<Script_Enemy>().m_OnRoute = true;
            m_Agent.SetDestination(m_StartingWaypoint.position);
            m_Agent.isStopped = false;
            p_State = BehaviorNodeState.RUNNING;
        }

        return p_State;
    }
    #endregion
}
