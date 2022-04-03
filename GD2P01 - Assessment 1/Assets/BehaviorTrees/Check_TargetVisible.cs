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
        if ((IsPlayerInViewCone() && m_Enemy.IsInCombat()) || m_Enemy.m_OnRoute)
        {
            p_State = BehaviorNodeState.SUCCESS;
        }
        else
        {
            p_State = BehaviorNodeState.FAILURE;
        }

        return p_State;
    }
    #endregion

    #region Private
    /// <summary>
    /// Checks if the player is inside the enemy viewcone
    /// </summary>
    /// <returns></returns>
    bool IsPlayerInViewCone()
    {
        if (m_Target)
        {
            // Get direction to player from enemy's head
            Vector3 dirToPlayer = (m_Target.position - (m_Transform.position + Vector3.up * 0.5f));
            // If angle from forward vector to direction is less than or equal to FOV and distance to player is less than vision distance
            if (Vector3.Angle(m_Transform.forward, dirToPlayer) <= m_Fov && dirToPlayer.magnitude <= m_VisionDistance)
            {
                // Shoot raycast in direction
                if (Physics.Raycast(m_Transform.position + Vector3.up * 0.5f, dirToPlayer.normalized, out m_VisionHit, m_VisionDistance))
                {
                    // If raycast hit player then set last known location, increase the alert level by one tick and look at the player.
                    // Return true if hit
                    if (m_VisionHit.transform.tag is "Player")
                    {
                        m_Enemy.GetManager().SetLastKnownLocation(m_Target.position);
                        m_Enemy.IncreaseAlert();
                        Debug.DrawLine(m_Transform.position + Vector3.up * 0.5f, m_VisionHit.point, Color.red);
                        m_Enemy.SetDirectionToPlayer(dirToPlayer);
                        m_Transform.LookAt(m_VisionHit.point);
                        return true;
                    }
                }
                // If first raycast failed, check from enemies weighst to player instead of from head
                dirToPlayer = (m_Target.position - (m_Transform.position));
                // Shoot raycast in direction from enemies head
                if (Physics.Raycast(m_Transform.position + Vector3.up * 0.5f, dirToPlayer.normalized, out m_VisionHit, m_VisionDistance))
                {
                    // If raycast hit player then set last known location, increase the alert level by one tick and look at the player.
                    // Return true if hit
                    if (m_VisionHit.transform.tag is "Player")
                    {
                        m_Enemy.GetManager().SetLastKnownLocation(m_Target.position);
                        m_Enemy.IncreaseAlert();
                        Debug.DrawLine(m_Transform.position + Vector3.up * 0.5f, m_VisionHit.point, Color.red);
                        m_Enemy.SetDirectionToPlayer(dirToPlayer);
                        m_Transform.LookAt(m_VisionHit.point);
                        return true;
                    }
                }
            }

        }

        // If enemy has not seen player shoot a fan of debug lines so that in scene view we can see the view cone.
        for (int i = 0; i < m_Fov * 2; i++)
        {
            Vector3 shootVec = m_Transform.rotation * Quaternion.AngleAxis(-1 * m_Fov + (i * m_Fov / m_Fov), Vector3.up) * Vector3.forward;
            Vector3 outPos = m_Transform.position + Vector3.up * 0.5f + shootVec * m_VisionDistance;
            Debug.DrawLine(m_Transform.position + Vector3.up * 0.5f, outPos, Color.green);
        }

        // If the enemy has not seen player then decrease alert level by one tick and return false
        m_Enemy.DecreaseAlert();

        return false;
    }
    #endregion
}
