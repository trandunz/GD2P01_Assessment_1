using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DoorTrigger : MonoBehaviour
{
    #region MemberVariables
    Script_EnemyManager m_EnemyManager;
    Transform m_Player;
    #endregion

    #region Private
    void Start()
    {
        // Grab player script
        m_Player = GameObject.FindWithTag("Player").transform;
        // Grab Enemy Manager script
        m_EnemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<Script_EnemyManager>();
    }
    void Update()
    {
        // If enemy manager exits and enemy is in range of door then open it. If player is not in the way of the door then close it behind the enemy
        if (m_EnemyManager != null)
        {
            if (EnemyInRange())
            {
                GetComponentInParent<Animator>().SetBool("Open", true);
            }
            else if (!PlayerInRange())
            {
                GetComponentInParent<Animator>().SetBool("Open", false);
            }
        }
    }
    /// <summary>
    /// Checks if any enemy is in range of the door
    /// </summary>
    /// <returns></returns>
    bool EnemyInRange()
    {
        foreach (Script_Enemy enemy in m_EnemyManager.GetEnemies())
        {
            if (enemy)
            {
                if ((enemy.transform.position - new Vector3(transform.position.x, enemy.transform.position.y, transform.position.z)).magnitude <= 2.0f)
                {
                    return true;
                }
            }


        }
        return false;
    }
    /// <summary>
    /// Checks if player is in range of the door
    /// </summary>
    /// <returns></returns>
    bool PlayerInRange()
    {
        if (m_Player)
        {
            if ((m_Player.position - new Vector3(transform.position.x, m_Player.position.y, transform.position.z)).magnitude <= 2.0f)
            {
                return true;
            }
        }

        return false;
    }
    #endregion
}
