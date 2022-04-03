using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorWaitUntil : BehaviorNode
{
    #region Public
    // Inherit BehaviorWaitUntil constructor from base class (behavior node)
    public BehaviorWaitUntil() : base(){}
    // Inherit BehaviorWaitUntil constructor from base class (behavior node)
    public BehaviorWaitUntil(BehaviorNode _child) : base(_child){}
    // Override BehaviorNode Evaluate and cater it to a WaitUntil type evaluation
    public override BehaviorNodeState Evaluate()
    {
        // evaluate Child node. If it fails, return failure else return running.
        switch (p_Children[0].Evaluate())
        {
            case BehaviorNodeState.FAILURE:
                p_State = BehaviorNodeState.FAILURE;
                return p_State;
            default:
                p_State = BehaviorNodeState.RUNNING;
                return p_State;
        }
    }
    #endregion
}
