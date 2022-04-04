// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Task_Chase.cs 
// Description : Sets The Agents Destination To The Player
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Task_Chase : BehaviorNode
{
    #region Member Variables
    NavMeshAgent m_Agent;
    Transform m_Target;
    #endregion

    #region Public
    public Task_Chase(NavMeshAgent _agent, Transform _target)
    {
        m_Agent = _agent;
        m_Target = _target;
    }
    public override BehaviorNodeState Evaluate()
    {
        // Set agents destination to the target / last seen player location
        m_Agent.SetDestination(m_Target.position);
        if (m_Agent)
        {
            m_Agent.isStopped = false;
        }

        p_State = BehaviorNodeState.RUNNING;
        return p_State;
    }
    #endregion
}
