using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Script_Enemy : TaskBehaviorTree
{
    public enum ENEMYTYPE
    {
        GUARD = 0,
        SWAT = 1,
        PUTIN = 2
    }
    #region Member Variables
    public bool m_SeenPlayer = false, m_OnRoute = false, m_InCombat = false;
    [SerializeField] float m_MaxHealth = 100.0f;
    [SerializeField] ENEMYTYPE m_EnemyType = ENEMYTYPE.GUARD;
    [SerializeField] float m_VisionDistance = 10.0f, m_DamageInterval_s = 0.2f, m_AlertSpeed_s = 1.0f, m_AlertDecay_s = 5.0f;
    [SerializeField] Transform[] m_WayPoints;
    [SerializeField] Script_Gun m_ActiveWeapon;
    Script_EnemyManager m_Manager;
    Script_Alarm m_Alarm;
    Transform m_Player;
    NavMeshAgent m_AttachedAgent;
    Transform m_HealthStation;
    Vector3 m_DirectionToPlayer;
    int m_RandomSelection;

    float m_Health = 100.0f, m_AlertLevel = 0.0f;
    bool m_TakingDamage = false, m_Healing = false;
    #endregion

    #region Public
    public ENEMYTYPE GetEnemyType()
    {
        return m_EnemyType;
    }
    public void Heal(float _amount)
    {
        if (!m_Healing)
        {
            StartCoroutine(HealRoutine(_amount));
        }
    }
    public float GetAlertSpeed()
    {
        if (m_InCombat)
            return m_AlertDecay_s;
        else
            return m_AlertSpeed_s;
    }
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
        if (m_Health < ((m_MaxHealth/2) + m_RandomSelection % 20))
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
        if (m_Health <= 0)
        {
            m_Manager.RemoveEnemy(this);
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
        m_ActiveWeapon.Fire(m_DirectionToPlayer);
    }
    public void SetDirectionToPlayer(Vector3 _direction)
    {
        m_DirectionToPlayer = _direction;
    }
    public Transform GetPlayerCache()
    {
        m_DirectionToPlayer = (m_Player.position - (transform.position + Vector3.up * 0.5f)).normalized;
        return m_Player;
    }
    #endregion

    #region Protected
    protected override BehaviorNode SetupTree()
    {
        m_Health = m_MaxHealth;
        m_Manager = GameObject.FindWithTag("EnemyManager").GetComponent<Script_EnemyManager>();
        m_Alarm = GameObject.FindWithTag("Alarm").GetComponent<Script_Alarm>();
        m_AttachedAgent = GetComponent<NavMeshAgent>();
        m_HealthStation = GameObject.FindGameObjectWithTag("HealthStation").transform;
        GameObject player = GameObject.FindWithTag("Player");

        if (player)
            m_Player = player.transform;
        
        switch(m_EnemyType)
        {
            case ENEMYTYPE.GUARD:
                {
                    return GuardTreeSetup();
                }
            case ENEMYTYPE.SWAT:
                {
                    return SwatTreeSetup();
                }
            case ENEMYTYPE.PUTIN:
                {
                    return PutinTreeSetup();
                }
            default:
                break;
        }
        return null;
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
        CheckForDeath();
        Random.InitState((int)Time.realtimeSinceStartup);
        m_RandomSelection = Random.Range(-9999, 9999);

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
                m_TakingDamage = false;
                yield return null;
            }
        }
        yield return new WaitForSeconds(m_DamageInterval_s);
        m_TakingDamage = false;
    }

    IEnumerator HealRoutine(float _amount)
    {
        m_Healing = true;
        for (int i = 0; i < _amount; _amount--)
        {
            if (m_Health < m_MaxHealth)
            {
                m_Health++;
            }
            else
            {
                m_Healing = false;
                yield return null;
            }
        }
        yield return new WaitUntil(() => m_Health >= m_MaxHealth);
        m_Healing = false;
    }
    BehaviorNode GuardTreeSetup()
    {
        BehaviorNode rootNode = new BehaviorSelector(new List<BehaviorNode>
        {
            new BehaviorSelector(new List<BehaviorNode>
            {
                
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetAlive(m_Player, this),
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
                                    new Task_Flee(m_AttachedAgent, m_HealthStation, m_Player, m_VisionDistance)
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
                                    new Task_Flee(m_AttachedAgent, m_HealthStation, m_Player, m_VisionDistance)
                                }),
                                new Task_Attack(this, m_Player)
                            })
                        }),
                        new Task_CallAlarm(m_AttachedAgent, m_Alarm, this)
                    })
                }),
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetAlive(m_Player, this),
                    new Check_TargetVisible(this, m_AttachedAgent, m_Player, m_VisionDistance),
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
                                    new Task_Flee(m_AttachedAgent, m_HealthStation, m_Player, m_VisionDistance)
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
                                    new Task_Flee(m_AttachedAgent, m_HealthStation, m_Player, m_VisionDistance)
                                }),
                                new Task_Attack(this, m_Player)
                            })
                        }),
                        new Task_CallAlarm(m_AttachedAgent, m_Alarm, this)
                    })
                }),
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetAlive(m_Player, this),
                    new Check_OthersInCombat(m_Manager),
                    new Task_MoveToTarget(m_AttachedAgent, m_Manager)
                })
            }),
            new Task_Patrol(m_AttachedAgent, m_WayPoints)
        });
        return rootNode;
    }
    BehaviorNode SwatTreeSetup()
    {
        BehaviorNode rootNode = new BehaviorSelector(new List<BehaviorNode>
        {
            new BehaviorSelector(new List<BehaviorNode>
            {

                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetAlive(m_Player, this),
                    new Check_GettingShot(this),
                    new Task_Attack(this, m_Player)
                }),
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetAlive(m_Player, this),
                    new Check_TargetVisible(this, m_AttachedAgent, m_Player, m_VisionDistance),
                    new Task_Attack(this, m_Player)
                }),
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetAlive(m_Player, this),
                    new Check_OthersInCombat(m_Manager),
                    new Task_MoveToTarget(m_AttachedAgent, m_Manager)
                })
            }),
            new Task_Patrol(m_AttachedAgent, m_WayPoints)
        });
        return rootNode;
    }
    BehaviorNode PutinTreeSetup()
    {
        BehaviorNode rootNode = new BehaviorSelector(new List<BehaviorNode>
        {
            new BehaviorSelector(new List<BehaviorNode>
            {

                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetAlive(m_Player, this),
                    new Check_GettingShot(this),
                    new Task_Attack(this, m_Player)
                }),
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_TargetAlive(m_Player, this),
                    new Check_TargetVisible(this, m_AttachedAgent, m_Player, m_VisionDistance),
                    new Task_Attack(this, m_Player)
                }),
                new BehaviorSequence(new List<BehaviorNode>
                {
                    new Check_AlarmCalled(m_Alarm),
                    new Task_Escape(this, GameObject.FindWithTag("Reinforcements").transform, m_WayPoints[0])
                })
            }),
            new Task_Patrol(m_AttachedAgent, m_WayPoints)
        });
        return rootNode;
    }
    #endregion
}
