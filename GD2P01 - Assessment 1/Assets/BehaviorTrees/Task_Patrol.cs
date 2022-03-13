using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Task_Patrol : BehaviorNode
{
    #region Member Variables
    NavMeshAgent m_Agent;
    Transform[] m_Waypoints;
    Vector3 m_CurrentWayPoint;
    const float m_MinStopDistance = 0.25f;
    int m_CurrentWaypointIndex = 0;
    #endregion

    #region Public
    public Task_Patrol(NavMeshAgent _agent, Transform[] _waypoints)
    {

        m_Agent = _agent;
        m_Waypoints = _waypoints;
        m_CurrentWaypointIndex = 0;
        m_CurrentWayPoint = m_Waypoints[m_CurrentWaypointIndex].position;
        SetAgentDestinationToWayPoint();
    }
    public override BehaviorNodeState Evaluate()
    {
        if (!IsEnemyOnRouteToWayPoint())
        {
            SetAgentDestinationToWayPoint();
        }
        if (HasReachedWaypoint())
        {
            SetWayPointToNext();
        }
        m_Agent.isStopped = false;

        p_State = BehaviorNodeState.RUNNING;
        return p_State;
    }
    #endregion

    #region Private
    void SetAgentDestinationToWayPoint()
    {
        m_Agent.SetDestination(m_CurrentWayPoint);
    }
    void SetWayPointToNext()
    {
        m_CurrentWaypointIndex++;
        if (m_CurrentWaypointIndex >= m_Waypoints.Length)
        {
            m_CurrentWaypointIndex = 0;
        }

        m_CurrentWayPoint = m_Waypoints[m_CurrentWaypointIndex].position;
        SetAgentDestinationToWayPoint();
    }
    bool HasReachedWaypoint()
    {
        if (m_Agent.remainingDistance <= m_MinStopDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }
    bool IsEnemyOnRouteToWayPoint()
    {
        foreach(Transform pos in m_Waypoints)
        {
            if (m_Agent.destination == pos.position)
            {
                return true;
            }
        }

        return false;
    }
    #endregion
}
