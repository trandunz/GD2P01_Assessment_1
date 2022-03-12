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
    Script_Enemy m_Enemy;
    #endregion

    #region Public
    public Check_TargetVisible(Script_Enemy _enemyScript, NavMeshAgent _agent, Transform _target, float _visionDistance)
    {
        m_Enemy = _enemyScript;
        m_Agent = _agent;
        m_Target = _target;
        m_VisionDistance = _visionDistance;
        m_Transform = m_Agent.transform;
    }
    public override BehaviorNodeState Evaluate()
    {
        if (IsPlayerInViewCone() || m_Enemy.m_OnRoute)
        {
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
        Vector3 dirToPlayer = (m_Target.position - (m_Transform.position + Vector3.up * 0.5f));
        if (Vector3.Angle(m_Transform.forward, dirToPlayer) <= m_Fov && dirToPlayer.magnitude <= m_VisionDistance)
        {
            if (Physics.Raycast(m_Transform.position + Vector3.up * 0.5f, dirToPlayer.normalized, out m_VisionHit, m_VisionDistance))
            {
                if (m_VisionHit.transform.tag is "Player")
                {
                    Debug.DrawLine(m_Transform.position + Vector3.up * 0.5f, m_VisionHit.point, Color.red);
                    m_Enemy.m_DirectionToPlayer = dirToPlayer;
                    return true;
                }
            }
            dirToPlayer = (m_Target.position - (m_Transform.position));
            if (Physics.Raycast(m_Transform.position + Vector3.up * 0.5f, dirToPlayer.normalized, out m_VisionHit, m_VisionDistance))
            {
                if (m_VisionHit.transform.tag is "Player")
                {
                    Debug.DrawLine(m_Transform.position + Vector3.up * 0.5f, m_VisionHit.point, Color.red);
                    m_Enemy.m_DirectionToPlayer = dirToPlayer;
                    return true;
                }
            }
        }

        for (int i = 0; i < m_Fov * 2; i++)
        {
            Vector3 shootVec = m_Transform.rotation * Quaternion.AngleAxis(-1 * m_Fov + (i * m_Fov / m_Fov), Vector3.up) * Vector3.forward;
            Vector3 outPos = m_Transform.position + Vector3.up * 0.5f + shootVec * m_VisionDistance;
            Debug.DrawLine(m_Transform.position + Vector3.up * 0.5f, outPos, Color.green);
        }

        return false;
    }
    #endregion
}
