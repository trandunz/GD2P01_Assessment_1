// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_Reinforcements.cs 
// Description : Reinforcements Implemention File
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Reinforcements : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] GameObject m_Unit;
    [SerializeField] GameObject m_Body;
    [SerializeField] Transform m_SpawnLocation;
    [SerializeField] Transform[] m_WayPoints;
    Script_EnemyManager m_EnemyManager;
    Animator m_Animator;
    #endregion

    #region Private
    private void Start()
    {
        // Grab and assign values
        m_EnemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<Script_EnemyManager>();
        m_Animator  = GetComponent<Animator>();
    }
    #endregion

    #region Public
    /// <summary>
    /// Starts the animation trigger for the swat van to pull up
    /// </summary>
    public void PullUp()
    {
        m_Animator.SetBool("PullUp", true);
    }
    public void EnableMesh()
    {
        m_Body.SetActive(true);
    }
    /// <summary>
    /// Spawns a Swat troop at the specified spawn location relative to the van.
    /// </summary>
    public void SpawnTroop()
    {
        Script_Enemy troop = Instantiate(m_Unit, m_SpawnLocation.position, Quaternion.identity).GetComponent<Script_Enemy>();
        troop.SetWaypoints(m_WayPoints);
        m_EnemyManager.AddEnemy(ref troop);
    }
    #endregion
}
