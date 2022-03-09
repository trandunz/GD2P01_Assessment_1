using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Check_TargetVisible : BehaviorNode
{
    #region Member Variables
    float m_VisionDistance = 10.0f, m_Fov = 45.0f;
    Transform m_Transform, m_Target;
    NavMeshAgent m_Agent;
    RaycastHit m_VisionHit;
    #endregion

    #region Public
    public Check_TargetVisible(NavMeshAgent _agent, Transform _target, float _visionDistance)
    {
        m_Agent = _agent;
        m_Target = _target;
        m_VisionDistance = _visionDistance;
        m_Transform = m_Agent.transform;
    }
    public override BehaviorNodeState Evaluate()
    {
        if (IsPlayerInViewCone())
        {
            Debug.Log("Player In View Cone!");
            m_Agent.isStopped = true;
            p_State = BehaviorNodeState.SUCCESS;
        }
        else
        {
            m_Agent.isStopped = false;
            p_State = BehaviorNodeState.FAILURE;
        }

        return p_State;
    }
    #endregion

    #region Private
    bool IsPlayerInViewCone()
    {
        bool m_TargetFound = false;
        RaycastHit hit;
        for (int i = 0; i < m_Fov; i++)
        {
            Vector3 shootVec = m_Transform.rotation * Quaternion.AngleAxis(-1 * m_Fov / 2 + (i * m_Fov / m_Fov), Vector3.up) * Vector3.forward;
            Vector3 outPos = m_Transform.position + shootVec * m_VisionDistance;
            if (Physics.Raycast(m_Transform.position, shootVec, out hit, m_VisionDistance))
            {
                Debug.DrawLine(m_Transform.position, hit.point, Color.green);
                if (hit.transform.tag == m_Target.tag)
                {
                    Debug.DrawLine(m_Transform.position, hit.point, Color.red);
                    m_TargetFound = true;
                }
            }
            else
            {
                Debug.DrawLine(m_Transform.position, outPos, Color.green);
            }
        }
        
        return m_TargetFound;
    }
    #endregion
}
