// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_EnemyManager.cs 
// Description : Enemy Manager Implementation File
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyManager : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] List<Script_Enemy> m_Enemies = new List<Script_Enemy>();
    Vector3 m_LastKnownLocation;
    Script_DialoguePopup m_DialoguePopupHandler;
    #endregion

    #region Public
    public List<Script_Enemy> GetEnemies()
    {
        return m_Enemies;
    }
    public  void RemoveEnemy(Script_Enemy _enemy)
    {
        m_Enemies.Remove(_enemy);
    }
    public void AddEnemy(ref Script_Enemy _enemy)
    {
        m_Enemies.Add(_enemy);
    }
    public void SetLastKnownLocation(Vector3 _location)
    {
        m_LastKnownLocation = _location;
    }
    /// <summary>
    /// Returns the last known location of the player.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetLastKnownLocation()
    {
        return m_LastKnownLocation;
    }
    /// <summary>
    /// Checks if any enemy in the scene is in combat, if so, return true else false
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Prints the enemy found message
    /// </summary>
    public void HandleFirstTimeScene()
    {
        m_DialoguePopupHandler.EnemyFoundMessage();
    }
    #endregion

    #region Private
    void Start()
    {
        // Grab and assign dialogue popup script
        m_DialoguePopupHandler = GameObject.FindWithTag("DialoguePopup").GetComponent<Script_DialoguePopup>();

        // Grab all enemies in the scene and addd them too the enemies list.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            m_Enemies.Add(enemies[i].GetComponent<Script_Enemy>());
        }

    }
    #endregion
}
