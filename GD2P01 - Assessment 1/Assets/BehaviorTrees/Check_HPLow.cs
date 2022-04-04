// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Check_HPLow.cs 
// Description : Checks If HP Is Low
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_HPLow : BehaviorNode
{
    #region Member Variables
    Script_Enemy m_EnemyScript;
    #endregion

    #region Public
    public Check_HPLow(Script_Enemy _enemyScript)
    {
        m_EnemyScript = _enemyScript;

    }
    public override BehaviorNodeState Evaluate()
    {

        if (m_EnemyScript.IsHealthLow())
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
}
