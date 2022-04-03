using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSequence : BehaviorNode
{
    #region MemberVariables
    bool m_AnyChildRunning = false;
    #endregion

    #region Public
    // Inherit BehaviorSequence constructor from base class (behavior node)
    public BehaviorSequence() : base(){}
    // Inherit BehaviorSequence constructor from base class (behavior node)
    public BehaviorSequence(List<BehaviorNode> _children) : base(_children){}
    // Override BehaviorNode Evaluate and cater it to a Sequence type evaluation
    public override BehaviorNodeState Evaluate()
    {
        // Reset any child running bool
        m_AnyChildRunning = false;

        // Loop thtrough all childeren nodes and evaluate. continue if any of them succeed and break out if any of them fail.
        foreach (BehaviorNode node in p_Children)
        {
            switch (node.Evaluate())
            {
                case BehaviorNodeState.FAILURE:
                    p_State = BehaviorNodeState.FAILURE;
                    return p_State;
                case BehaviorNodeState.RUNNING:
                    m_AnyChildRunning = true;
                    continue;
                default:
                    continue;
            }
        }

        // If any child is running then return selector as running else if all succeed then return success
        p_State = m_AnyChildRunning ? BehaviorNodeState.RUNNING : BehaviorNodeState.SUCCESS;

        return p_State;
    }
    #endregion
}
