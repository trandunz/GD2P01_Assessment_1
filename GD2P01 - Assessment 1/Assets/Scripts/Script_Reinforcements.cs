using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Reinforcements : MonoBehaviour
{
    [SerializeField] GameObject m_Unit;
    [SerializeField] GameObject m_Body;
    [SerializeField] Transform m_SpawnLocation;
    [SerializeField] Transform[] m_WayPoints;
    Script_EnemyManager m_EnemyManager;
    Animator m_Animator;
    private void Start()
    {
        m_EnemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<Script_EnemyManager>();
        m_Animator  = GetComponent<Animator>();
    }

    public void PullUp()
    {
        m_Animator.SetBool("PullUp", true);
    }
    public void EnableMesh()
    {
        m_Body.SetActive(true);
    }
    public void SpawnTroop()
    {
        Script_Enemy troop = Instantiate(m_Unit, m_SpawnLocation.position, Quaternion.identity).GetComponent<Script_Enemy>();
        troop.SetWaypoints(m_WayPoints);
        m_EnemyManager.AddEnemy(ref troop);
    }
}
