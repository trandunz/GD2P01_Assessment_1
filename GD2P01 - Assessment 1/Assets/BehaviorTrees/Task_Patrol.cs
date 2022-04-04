// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Task_Patrol.cs 
// Description : Entity Patrols Through Given Waypoints
// Author : William Inman
// Mail : william.inman@mds.ac.nz

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

        // Start the navmesh agent off on the path to the first waypoint
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
    /// <summary>
    /// Checks if enemy is currently on the way to any of the waypoints
    /// </summary>
    /// <returns></returns>
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
