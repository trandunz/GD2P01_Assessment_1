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
        m_Agent.SetDestination(m_Alarm.transform.position);
        if (m_Agent)
        {
            m_Agent.isStopped = false;
        }
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
