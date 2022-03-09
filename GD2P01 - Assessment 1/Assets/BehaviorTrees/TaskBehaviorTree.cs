using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskBehaviorTree : MonoBehaviour
{
    #region Member Variables
    BehaviorNode m_RootNode = null;
    #endregion

    #region Protected
    protected void Start()
    {
        m_RootNode = SetupTree();
    }
    protected void Update()
    {
        if (m_RootNode != null)
            m_RootNode.Evaluate();
    }
    protected abstract BehaviorNode SetupTree();
    #endregion
}