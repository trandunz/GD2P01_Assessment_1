using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Script_Enemy : TaskBehaviorTree
{
    #region Member Variables
    [SerializeField] float m_VisionDistance = 10.0f;
    [SerializeField] Transform[] m_WayPoints;
    #endregion

    #region Protected
    protected override BehaviorNode SetupTree()
    {
        BehaviorNode rootNode = new BehaviorSelector(new List<BehaviorNode>
        {
            new Check_TargetVisible(GetComponent<NavMeshAgent>(), GameObject.FindGameObjectWithTag("Player").transform, m_VisionDistance),
            new Task_Patrol(GetComponent<NavMeshAgent>(), m_WayPoints)
        });

        return rootNode;
    }
    #endregion
}
