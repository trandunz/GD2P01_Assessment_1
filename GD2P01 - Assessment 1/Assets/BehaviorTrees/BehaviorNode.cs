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

    Dictionary<string, object> m_DataDictionary = new Dictionary<string, object>();
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
    protected object GetData(string _key)
    {
        object value;

        if (m_DataDictionary.TryGetValue(_key, out value))
            return value;

        BehaviorNode node = Parent;
        while (node != null)
        {
            value = node.GetData(_key);
            if (value != null)
                return value;
            else
                node = node.Parent;
        }
        return null;
    }
    protected bool ClearData(string _key)
    {
        if (m_DataDictionary.ContainsKey(_key))
        {
            m_DataDictionary.Remove(_key);
            return true;
        }

        BehaviorNode node = Parent;
        while (node != null)
        {
            bool cleared = node.ClearData(_key);

            if (cleared)
                return true;
            else
                node = node.Parent;
        }
        return false;
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