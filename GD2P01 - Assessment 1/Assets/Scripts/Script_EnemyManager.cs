using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyManager : MonoBehaviour
{
    [SerializeField] List<Script_Enemy> m_Enemies = new List<Script_Enemy>();
    Vector3 m_LastKnownLocation;

    #region Public
    public List<Script_Enemy> GetEnemies()
    {
        return m_Enemies;
    }
    public void AddEnemy(ref Script_Enemy _enemy)
    {
        m_Enemies.Add(_enemy);
    }
    public void SetLastKnownLocation(Vector3 _location)
    {
        m_LastKnownLocation = _location;
    }
    public Vector3 GetLastKnownLocation()
    {
        return m_LastKnownLocation;
    }
    public bool IsEnemyInCombat()
    {
        foreach (Script_Enemy enemy in m_Enemies)
        {
            if (enemy)
            {
                if (enemy.IsInCombat())
                {
                    return true;
                }
            }
            
        }
        return false;
    }
    #endregion

    #region Private
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            m_Enemies.Add(enemies[i].GetComponent<Script_Enemy>());
        }

    }
    private void Update()
    {
    }
    #endregion
}
