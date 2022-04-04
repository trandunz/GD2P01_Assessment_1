// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_WinCondition.cs 
// Description : Win Condition Implemention File
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_WinCondition : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] bool m_Tutorial = false;
    Script_EnemyManager m_EnemyManager;
    #endregion

    #region Public
    public void SwitchToLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void SwitchToTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void SwitchToMissionFailed()
    {
        SceneManager.LoadScene(3);
    }
    public void SwitchToMissionSuccess()
    {
        SceneManager.LoadScene(4);
    }
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    #region Private
    void Start()
    {
        // Grab and assign enemy manager
        m_EnemyManager = GameObject.FindObjectOfType<Script_EnemyManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        // If player exits through entry vent and boss is dead then mission success, else mission failed.
        // if in tutorial then take player to main level regardless.
        if (other.transform.root.gameObject.tag == "Player" && IsBossDead())
        {
            if (m_Tutorial)
            {
                SwitchToLevel();
            }
            else
                SwitchToMissionSuccess();
        }
        else if (other.transform.root.gameObject.tag == "Player" && !IsBossDead())
        {
            if (m_Tutorial)
            {
                SwitchToLevel();
            }
            else
                SwitchToMissionFailed();
        }
    }
    void OnTriggerStay(Collider other)
    {
        // Called if enter failed
        // If player exits through entry vent and boss is dead then mission success, else mission failed.
        // if in tutorial then take player to main level regardless.
        if (other.transform.root.gameObject.tag == "Player" && IsBossDead())
        {
            if (m_Tutorial)
            {
                SwitchToLevel();
            }
            else
                SwitchToMissionSuccess();
        }
        else if(other.transform.root.gameObject.tag == "Player" && !IsBossDead())
        {
            if (m_Tutorial)
            {
                SwitchToLevel();
            }
            else
                SwitchToMissionFailed();
        }

    }
    /// <summary>
    /// Checks through all alive enemies for the boss. If he is found return false else true.
    /// </summary>
    /// <returns></returns>
    bool IsBossDead()
    {
        foreach (Script_Enemy enemy in m_EnemyManager.GetEnemies())
        {
            if (enemy.GetEnemyType() == Script_Enemy.ENEMYTYPE.BOSS)
            {
                return false;
            }
        }
        return true;
    }
    #endregion
}
