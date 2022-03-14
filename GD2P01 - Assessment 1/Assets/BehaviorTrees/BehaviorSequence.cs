using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSequence : BehaviorNode
{
    #region Public
    public BehaviorSequence() : base() { }
    public BehaviorSequence(List<BehaviorNode> _children) : base(_children) { }
    public override BehaviorNodeState Evaluate()
    {
        bool anyChildIsRunning = false;

        foreach (BehaviorNode node in p_Children)
        {
            switch (node.Evaluate())
            {
                case BehaviorNodeState.FAILURE:
                    p_State = BehaviorNodeState.FAILURE;
                    return p_State;
                case BehaviorNodeState.RUNNING:
                    anyChildIsRunning = true;
                    continue;
                default:
                    continue;
            }
        }

        p_State = anyChildIsRunning ? BehaviorNodeState.RUNNING : BehaviorNodeState.SUCCESS;

        return p_State;
    }
    #endregion
}
