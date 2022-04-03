using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task_Flee : BehaviorNode
{
    #region Member Variables
    NavMeshAgent m_Agent;
    Transform m_Target;
    Transform m_HealthStation;
    float m_DetectionRange;
    #endregion

    #region Public
    public Task_Flee(NavMeshAgent _agent, Transform _healthStation, Transform _target, float _detectionRange)
    {
        m_Agent = _agent;
        m_Target = _target;
        m_HealthStation = _healthStation;
        m_DetectionRange = _detectionRange;
    }
    public override BehaviorNodeState Evaluate()
    {
        // If ttarget exists
        if (m_Target)
        {
            // If distance to player is less than or equal to the detection range / 2 then return node failure and consiquently attack. Also set alert level max and look at the player
            if ((m_Target.position - m_Agent.transform.position).magnitude <= m_DetectionRange / 2)
            {
                m_Agent.GetComponent<Script_Enemy>().SetAlertMax();
                m_Agent.GetComponent<Script_Enemy>().m_OnRoute = true;
                m_Agent.transform.LookAt(new Vector3(m_Target.position.x, m_Agent.transform.position.y, m_Target.position.z));
                {
                    if (m_Agent.isActiveAndEnabled)
                    {
                        m_Agent.velocity = Vector3.zero;
                        m_Agent.isStopped = true;
                    }
                }
                Debug.Log("Attack!");
                p_State = BehaviorNodeState.FAILURE;
            }
            // If distance to player is more than detection range / 2 then retreate to the health machine.
            else
            {
                m_Agent.GetComponent<Script_Enemy>().m_OnRoute = true;
                
                if (m_Agent.isActiveAndEnabled)
                {
                    m_Agent.isStopped = false;
                    m_Agent.SetDestination(m_HealthStation.position);
                    // If distance to health machine is less than 1 unit then heal up to full HP
                    if ((m_HealthStation.position - m_Agent.transform.position).magnitude <= 1.0f)
                    {
                        m_Agent.GetComponent<Script_Enemy>().Heal(m_Agent.GetComponent<Script_Enemy>().GetMaxHealth());
                        m_Agent.GetComponent<Script_Enemy>().m_OnRoute = false;
                    }
                }
                Debug.Log("Retreating!");
                p_State = BehaviorNodeState.RUNNING;
            }
        }

        return p_State;
    }
    #endregion
}
