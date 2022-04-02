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
        m_EnemyManager = GameObject.FindObjectOfType<Script_EnemyManager>();
    }
    void OnTriggerEnter(Collider other)
    {
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
    bool IsBossDead()
    {
        foreach (Script_Enemy enemy in m_EnemyManager.GetEnemies())
        {
            if (enemy.GetEnemyType() == Script_Enemy.ENEMYTYPE.PUTIN)
            {
                return false;
            }
        }
        return true;
    }
    #endregion
}
