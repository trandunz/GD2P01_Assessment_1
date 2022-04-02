using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviorNodeState
{
    FAILURE,
    RUNNING,
    SUCCESS
}

public class BehaviorNode
{
    #region Member Variables
    public BehaviorNode Parent = null;

    protected BehaviorNodeState p_State;
    protected List<BehaviorNode> p_Children = new List<BehaviorNode>();
    #endregion

    #region Public
    public virtual BehaviorNodeState Evaluate() => BehaviorNodeState.FAILURE;
    #endregion

    #region Protected
    protected BehaviorNode() {}
    protected BehaviorNode(List<BehaviorNode> _children)
    {
        foreach (BehaviorNode child in _children)
            AttachNode(child);
    }
    protected BehaviorNode(BehaviorNode _child)
    {
        AttachNode(_child);
    }
    #endregion

    #region Private
    void AttachNode(BehaviorNode _node)
    {
        _node.Parent = this;
        p_Children.Add(_node);
    }
    #endregion
}