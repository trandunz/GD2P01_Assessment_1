// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : BehaviorNode.cs 
// Description : BehaviorNode Implementation
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorNode
{
    #region Member Variables
    // Parent Node
    public BehaviorNode Parent = null;

    // Current Node's State
    protected BehaviorNodeState p_State = BehaviorNodeState.FAILURE;
    // List of nodes corresponding to itts children
    protected List<BehaviorNode> p_Children = new List<BehaviorNode>();
    #endregion

    #region Public
    /// <summary>
    /// enum of node states
    /// </summary>
    public enum BehaviorNodeState
    {
        FAILURE,
        RUNNING,
        SUCCESS
    }
    /// <summary>
    /// Initialized the evaluate function with default state value
    /// </summary>
    /// <returns></returns>
    public virtual BehaviorNodeState Evaluate() => p_State;
    #endregion

    #region Protected
    protected BehaviorNode() {}
    /// <summary>
    /// Behavior node contructor that adds all given nodes the child list
    /// </summary>
    /// <param name="_children"></param>
    protected BehaviorNode(List<BehaviorNode> _children)
    {
        AttachNodes(_children);
    }
    /// <summary>
    /// Behavior Node Constructor that adds the singular given node to the child list
    /// </summary>
    /// <param name="_child"></param>
    protected BehaviorNode(BehaviorNode _child)
    {
        AttachNode(_child);
    }
    #endregion

    #region Private
    /// <summary>
    /// Adds the given node to the current nodes child list
    /// </summary>
    /// <param name="_node"></param>
    void AttachNode(BehaviorNode _node)
    {
        // Sets tthe given nodes parent to the current node
        _node.Parent = this;
        // Add the node to the list of children
        p_Children.Add(_node);
    }
    /// <summary>
    /// Adds the given nodes to the current nodes child list
    /// </summary>
    /// <param name="_nodes"></param>
    void AttachNodes(List<BehaviorNode> _nodes)
    {
        foreach (BehaviorNode node in _nodes)
        {
            AttachNode(node);
        }
    }
    #endregion
}