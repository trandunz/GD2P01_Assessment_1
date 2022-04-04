// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Task_Attack.cs 
// Description : Attacks The Target
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task_Attack : BehaviorNode
{
    #region Member Variables
    Script_Enemy m_EnemyScript;
    Transform m_Transform;
    Transform m_Target;
    NavMeshAgent m_Agent;
    #endregion

    #region Public
    public Task_Attack(Script_Enemy _enemyScript, Transform _target)
    {
        m_EnemyScript = _enemyScript;
        m_Transform = m_EnemyScript.transform;
        m_Target = _target;
        m_Agent = m_EnemyScript.GetComponent<NavMeshAgent>();
    }
    public override BehaviorNodeState Evaluate()
    {
        // If the target exists
        if (m_Target)
        {
            // If enemy is in combat then set alert for all enemies to maximum
            if (m_EnemyScript.IsInCombat())
            {
                foreach (Script_Enemy enemy in m_EnemyScript.GetManager().GetEnemies())
                {
                    enemy.SetAlertMax();
                }
            }

            // Look at the target
            m_Transform.LookAt(new Vector3(m_Target.position.x, m_Transform.position.y, m_Target.position.z));

            // Fire a bullet forward
            m_EnemyScript.FireBullet();

            // Stop the navmesh agent in its tracks and reset its path so that it doesent continue.
            if (m_Agent.isActiveAndEnabled)
            {
                m_Agent.velocity = Vector3.zero;
                m_Agent.isStopped = true;
                m_Agent.ResetPath();
            }
        }

        p_State = BehaviorNodeState.RUNNING;

        return p_State;
    }
    #endregion
}
