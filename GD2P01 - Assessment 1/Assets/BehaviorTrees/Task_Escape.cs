// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Task_Escape.cs 
// Description : Sets The Agents Destination To An Escape Point
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine.SceneManagement;

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
        // Evaluate the random value between 0 and 1
        switch(m_RandomSelection % 2)
        {
            // Escape To Van
            case 0:
                {
                    m_Agent.SetDestination(m_Van.position);
                    m_Agent.isStopped = false;
                    // If distance to van is less than 2 units then switch scenes to mission failed
                    if ((m_Van.position - m_Transform.position).magnitude <= 2.0f)
                    {
                        SceneManager.LoadScene(5);
                    }
                    break;
                }
            // Escape to saferoom
            case 1:
                {
                    m_Agent.SetDestination(m_SafeRoom.position);
                    m_Agent.isStopped = false;
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
