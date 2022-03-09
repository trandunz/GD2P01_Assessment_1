using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Task_Patrol : BehaviorNode
{
    #region Member Variables
    NavMeshAgent m_Agent;
    Transform[] m_Waypoints;
    Transform m_CurrentWayPoint;
    const float m_MinStopDistance = 0.1f;
    int m_CurrentWaypointIndex = 0;
    #endregion

    #region Public
    public Task_Patrol(NavMeshAgent _agent, Transform[] _waypoints)
    {

        m_Agent = _agent;
        m_Waypoints = _waypoints;
        m_CurrentWaypointIndex = 0;
        m_CurrentWayPoint = m_Waypoints[m_CurrentWaypointIndex];
        SetAgentDestinationToWayPoint();
    }
    public override BehaviorNodeState Evaluate()
    {
        if (HasReachedWaypoint())
        {
            SetWayPointToNext();
        }

        p_State = BehaviorNodeState.RUNNING;
        return p_State;
    }
    #endregion

    #region Private
    void SetAgentDestinationToWayPoint()
    {
        m_Agent.SetDestination(new Vector3(m_CurrentWayPoint.position.x, m_Agent.transform.position.y, m_CurrentWayPoint.position.z));
    }
    void SetWayPointToNext()
    {
        m_CurrentWaypointIndex += 1;
        if (m_CurrentWaypointIndex >= m_Waypoints.Length)
        {
            m_CurrentWaypointIndex = 0;
        }

        m_CurrentWayPoint = m_Waypoints[m_CurrentWaypointIndex];
        SetAgentDestinationToWayPoint();
    }
    bool HasReachedWaypoint()
    {
        if (m_CurrentWayPoint.position.x - m_Agent.transform.position.x <= m_MinStopDistance && m_CurrentWayPoint.position.z - m_Agent.transform.position.z <= m_MinStopDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }
    #endregion
}
