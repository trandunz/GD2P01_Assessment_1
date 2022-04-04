// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : BehaviorSelector.cs 
// Description : BehaviorSelector Implementation
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSelector : BehaviorNode
{
    #region Public
    // Inherit BehaviorSelector constructor from base class (behavior node)
    public BehaviorSelector() : base(){}
    // Inherit BehaviorSelector constructor from base class (behavior node)
    public BehaviorSelector(List<BehaviorNode> _children) : base(_children){}
    // Override BehaviorNode Evaluate and cater it to a Selector type evaluation
    public override BehaviorNodeState Evaluate()
    {
        // Loop thtrough all childeren nodes and evaluate. continue if any of them fail and break out if any of them are running or returning success.
        foreach (BehaviorNode node in p_Children)
        {
            switch (node.Evaluate())
            {
                case BehaviorNodeState.SUCCESS:
                    p_State = BehaviorNodeState.SUCCESS;
                    return p_State;
                case BehaviorNodeState.RUNNING:
                    p_State = BehaviorNodeState.RUNNING;
                    return p_State;
                default:
                    continue;
            }
        }

        // If all nodes are evaluated then return failure
        p_State = BehaviorNodeState.FAILURE;

        return p_State;
    }
    #endregion
}