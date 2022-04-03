using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBehaviorTree : MonoBehaviour
{
    #region Member Variables
    BehaviorNode m_RootNode = null;
    #endregion

    #region Protected
    protected abstract BehaviorNode SetupTree();
    // Call and assign the root node to the overided settup tree's root node from the enemy / behavior tree setup script
    protected void Start()
    {
        m_RootNode = SetupTree();
    }
    protected void Update()
    {
        // If the root node is not null then constantly evaluate and transverse the behavior tree
        if (m_RootNode != null)
            m_RootNode.Evaluate();
    }
    #endregion
}