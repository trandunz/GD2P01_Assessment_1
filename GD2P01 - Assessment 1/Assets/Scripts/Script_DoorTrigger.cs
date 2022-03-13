using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DoorTrigger : MonoBehaviour
{
    Script_EnemyManager m_EnemyManager;
    Transform m_Player;
    private void Start()
    {
        m_Player = GameObject.FindWithTag("Player").transform;
        m_EnemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<Script_EnemyManager>();
    }

    private void Update()
    {
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
}
