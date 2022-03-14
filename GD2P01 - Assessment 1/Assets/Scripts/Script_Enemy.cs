using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Script_Enemy : TaskBehaviorTree
{
    #region Member Variables
    public bool m_SeenPlayer = false, m_OnRoute = false, m_InCombat = false;
    [SerializeField] static float m_MaxHealth = 100.0f;
    [SerializeField] float m_VisionDistance = 10.0f, m_DamageInterval_s = 0.2f, m_AlertSpeed_s = 1.0f, m_AlertDecay_s = 5.0f;
    [SerializeField] Transform[] m_WayPoints;
    [SerializeField] Script_Gun m_ActiveWeapon;
    Script_EnemyManager m_Manager;
    Script_Alarm m_Alarm;
    Transform m_Player;
    Vector3 m_DirectionToPlayer;
    
    float m_Health = m_MaxHealth, m_AlertLevel = 0.0f;
    bool m_TakingDamage = false;
    #endregion

    #region Public
    public void SetAlertMax()
    {
        if (m_Player)
            m_Manager.SetLastKnownLocation(m_Player.position);
        m_AlertLevel = m_AlertDecay_s;
        m_InCombat = true;
    }
    public void SetWaypoints(Transform[] _waypoints)
    {
        m_WayPoints = _waypoints;
    }
    public ref Script_EnemyManager GetManager()
    {
        return ref m_Manager;
    }
    public float GetHealth()
    {
        return m_Health;
    }
    public float GetMaxHealth()
    {
        return m_MaxHealth;
    }
    public bool HealthLow()
    {
        if (m_Health < 50.0f)
        {
            return true;
        }
        return false;
    }
    public bool IsTakingDamage()
    {
        return m_TakingDamage;
    }
    public void IncreaseAlert()
    {
        if (m_AlertLevel >= m_AlertSpeed_s)
        {
            m_AlertLevel = m_AlertDecay_s;
            m_InCombat = true;
        }
        else
        {
            m_AlertLevel += Time.deltaTime;
        }
    }
    public void CheckForDeath()
    {
        if (m_Health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
    public void DecreaseAlert()
    {
        if (m_AlertLevel > 0.0f)
        {
            m_AlertLevel -= Time.deltaTime;
        }
        else
        {
            m_InCombat = false;
            m_AlertLevel = 0.0f;
        }
    }
    public bool IsInCombat()
    {
        return m_InCombat;
    }
    public float GetAlertLevel()
    {
        return m_AlertLevel;
    }
    public void FireBullet()
    {
        m_ActiveWeapon.Fire();
    }
    public void SetDirectionToPlayer(Vector3 _direction)
    {
        m_DirectionToPlayer = _direction;
    }
    #endregion

    #region Protected
    protected override BehaviorNode SetupTree()
    {
        m_Manager = GameObject.FindWithTag("EnemyManager").GetComponent<Script_EnemyManager>();
        m_Alarm = GameObject.FindWithTag("Alarm").GetComponent<Script_Alarm>();
        NavMeshAgent attachedAgent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player)
            m_Player = player.transform;
        BehaviorNode rootNode = new BehaviorSelector(new List<BehaviorNode>
        {
            new BehaviorSelector(new List<BehaviorNode>
            {
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_GettingShot(this),
                    new BehaviorSelector(new List<BehaviorNode>
                    {
                        new BehaviorSequence(new List<BehaviorNode>
                        {
                            new Check_AlarmCalled(m_Alarm),
                            new BehaviorSelector(new List<BehaviorNode>
                            {
                                new BehaviorSequence(new List<BehaviorNode>
                                {
                                    new Check_HPLow(this),
                                    new Task_Flee(attachedAgent, m_WayPoints[0], m_Player)
                                }),
                                new Task_Attack(this, m_Player)
                            })
                        }),
                        new BehaviorSequence(new List<BehaviorNode>
                        {
                            new Check_GuardOnWay(m_Alarm, this),
                            new BehaviorSelector(new List<BehaviorNode>
                            {
                                new BehaviorSequence(new List<BehaviorNode>
                                {
                                    new Check_HPLow(this),
                                    new Task_Flee(attachedAgent, m_WayPoints[0], m_Player)
                                }),
                                new Task_Attack(this, m_Player)
                            })
                        }),
                        new Task_CallAlarm(attachedAgent, m_Alarm, this)
                    })
                }),
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetVisible(this, attachedAgent, m_Player, m_VisionDistance),
                    new BehaviorSelector(new List<BehaviorNode>
                    {
                        new BehaviorSequence(new List<BehaviorNode>
                        {
                            new Check_AlarmCalled(m_Alarm),
                            new BehaviorSelector(new List<BehaviorNode>
                            {
                                new BehaviorSequence(new List<BehaviorNode>
                                {
                                    new Check_HPLow(this),
                                    new Task_Flee(attachedAgent, m_WayPoints[0], m_Player)
                                }),
                                new Task_Attack(this, m_Player)
                            })
                        }),
                        new BehaviorSequence(new List<BehaviorNode>
                        {
                            new Check_GuardOnWay(m_Alarm, this),
                            new BehaviorSelector(new List<BehaviorNode>
                            {
                                new BehaviorSequence(new List<BehaviorNode>
                                {
                                    new Check_HPLow(this),
                                    new Task_Flee(attachedAgent, m_WayPoints[0], m_Player)
                                }),
                                new Task_Attack(this, m_Player)
                            })
                        }),
                        new Task_CallAlarm(attachedAgent, m_Alarm, this)
                    })
                }),
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_OthersInCombat(m_Manager),
                    new Task_MoveToTarget(attachedAgent, m_Manager)
                })
            }),
            new Task_Patrol(attachedAgent, m_WayPoints)
        }) ;

        return rootNode;
    }
    #endregion

    #region Private
    void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Bullet")
        { 
            Script_Bullet bulletScript = _other.transform.GetComponent<Script_Bullet>();
            if (bulletScript.IsFriendly())
            {
                if (!m_TakingDamage)
                    StartCoroutine(DamageRoutine(bulletScript.GetDamage()));

            }
        }
    }
    void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.tag == "Bullet")
        {
            Script_Bullet bulletScript = _other.transform.GetComponent<Script_Bullet>();
            if (bulletScript.IsFriendly())
            {
                if (!m_TakingDamage)
                    StartCoroutine(DamageRoutine(bulletScript.GetDamage()));

            }
        }
    }
    IEnumerator DamageRoutine(float _amount)
    {
        m_TakingDamage = true;
        if (m_Player)
            m_Manager.SetLastKnownLocation(m_Player.position);
        for (int i = 0; i < _amount; _amount--)
        {
            if (m_Health > 0)
            {
                m_Health--;
            }
            else
            {
                yield return null;
                CheckForDeath();
            }
        }
        yield return new WaitForSeconds(m_DamageInterval_s);
        m_TakingDamage = false;
    }
    #endregion
}
