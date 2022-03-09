using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorWaitUntil : BehaviorNode
{
    #region Public
    public BehaviorWaitUntil() : base() { }
    public BehaviorWaitUntil(BehaviorNode _child) : base(_child) { }
    public override BehaviorNodeState Evaluate()
    {
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
