using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Script_Enemy : MonoBehaviour
{
    [SerializeField] Transform[] m_WayPoints;
    const float m_MinStopDistance = 1.0f;
    NavMeshAgent m_Agent;
    Transform m_CurrentWayPoint;
    int m_CurrentWaypointIndex = 0;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_CurrentWayPoint = m_WayPoints[m_CurrentWaypointIndex];
        m_Agent.SetDestination(m_CurrentWayPoint.position);
    }

    private void Update()
    {
        if (HasReachedWaypoint())
        {
            SetWayPoint();
        }
    }

    bool HasReachedWaypoint()
    {
        if (new Vector3(m_CurrentWayPoint.position.x - transform.position.x, transform.position.y, m_CurrentWayPoint.position.z - transform.position.z).magnitude <= m_MinStopDistance)
            return true;
        else
            return false;
    }

    void SetWayPoint()
    {
        m_CurrentWaypointIndex++;
        if (m_CurrentWaypointIndex >= m_WayPoints.Length)
            m_CurrentWaypointIndex = 0;

        m_CurrentWayPoint = m_WayPoints[m_CurrentWaypointIndex];
        m_Agent.SetDestination(new Vector3(m_CurrentWayPoint.position.x, transform.position.y, m_CurrentWayPoint.position.z));
    }
}
