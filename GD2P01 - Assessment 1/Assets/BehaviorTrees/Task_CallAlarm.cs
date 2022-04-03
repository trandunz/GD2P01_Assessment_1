using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task_CallAlarm : BehaviorNode
{
    #region Member Variables
    Script_Alarm m_Alarm;
    NavMeshAgent m_Agent;
    Script_Enemy m_Enemy;
    bool m_FirstTimeScene = false;
    #endregion

    #region Public
    public Task_CallAlarm(NavMeshAgent _agent, Script_Alarm _alarm, Script_Enemy _enemyScript)
    {
        m_Enemy = _enemyScript;
        m_Alarm = _alarm;
        m_Agent = _agent;
    }
    public override BehaviorNodeState Evaluate()
    {
        // If its the first time any enemy has seen the player then trigger diologue popup
        if (!m_FirstTimeScene)
        {
            m_Enemy.GetManager().HandleFirstTimeScene();
            m_FirstTimeScene = true;
        }
        
        // Set the agents destination to the alarm and start him moving
        m_Agent.SetDestination(m_Alarm.transform.position);
        if (m_Agent)
        {
            m_Agent.isStopped = false;
        }

        // if the distance to the alarm is less than 1 unit then trigger the alarm
        float distance = (new Vector3(m_Alarm.transform.position.x, m_Agent.transform.position.y, m_Alarm.transform.position.z) - m_Agent.transform.position).magnitude;
        if (distance <= 1.0f)
        {
            m_Enemy.m_OnRoute = false;
            m_Alarm.Trigger();
        }

        p_State = BehaviorNodeState.RUNNING;

        return p_State;
    }
    #endregion
}
