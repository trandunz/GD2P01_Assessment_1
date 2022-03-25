using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task_Escape : BehaviorNode
{
    #region Member Variables
    Script_Enemy m_EnemyScript;
    Transform m_Transform;
    Transform m_Van;
    Transform m_SafeRoom;
    NavMeshAgent m_Agent;
    int m_RandomSelection;
    #endregion

    #region Public
    public Task_Escape(Script_Enemy _enemyScript, Transform _vanTransform, Transform _safeRoom)
    {
        Random.InitState((int)Time.realtimeSinceStartup);
        m_EnemyScript = _enemyScript;
        m_Transform = m_EnemyScript.transform;
        m_Van = _vanTransform;
        m_SafeRoom = _safeRoom;
        m_Agent = m_EnemyScript.GetComponent<NavMeshAgent>();
        m_RandomSelection = Random.Range(0, 9999);
        Debug.Log(m_RandomSelection % 2);
    }
    public override BehaviorNodeState Evaluate()
    {
        switch(m_RandomSelection % 2)
        {
            // Escape To Van
            case 0:
                {
                    m_Agent.SetDestination(m_Van.position);
                    m_Agent.isStopped = false;
                    if ((m_Van.position - m_Transform.position).magnitude <= 2.0f)
                    {
                        Debug.Log("End Game");
                    }
                    break;
                }
            // Escape to saferoom
            case 1:
                {
                    m_Agent.SetDestination(m_SafeRoom.position);
                    m_Agent.isStopped = false;
                    if ((m_SafeRoom.position - m_Transform.position).magnitude <= 2.0f)
                    {
                        Debug.Log("End Game");
                    }
                    break;
                }
            default:
                break;
        }

        p_State = BehaviorNodeState.RUNNING;

        return p_State;
    }
    #endregion
}
