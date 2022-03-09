using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSelector : BehaviorNode
{
    #region Public
    public BehaviorSelector() : base() { }
    public BehaviorSelector(List<BehaviorNode> _children) : base(_children) { }
    public override BehaviorNodeState Evaluate()
    {
        foreach (BehaviorNode node in p_Children)
        {
            switch (node.Evaluate())
            {
                case BehaviorNodeState.FAILURE:
                    continue;
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

        p_State = BehaviorNodeState.FAILURE;

        return p_State;
    }
    #endregion
}