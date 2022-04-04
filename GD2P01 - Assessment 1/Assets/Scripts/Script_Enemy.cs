// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_Enemy.cs 
// Description : Generalised Enemy Implementation File
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Script_Enemy : TaskBehaviorTree
{
    #region Member Variables
    public bool m_SeenPlayer = false, m_OnRoute = false, m_InCombat = false;
    [SerializeField] float m_MaxHealth = 100.0f;
    [SerializeField] ENEMYTYPE m_EnemyType = ENEMYTYPE.GUARD;
    [SerializeField] float m_VisionDistance = 10.0f, m_DamageInterval_s = 0.2f, m_AlertSpeed_s = 1.0f, m_AlertDecay_s = 5.0f;
    [SerializeField] Transform[] m_WayPoints;
    [SerializeField] Script_Gun m_ActiveWeapon;
    [SerializeField] AudioClip m_HitSound;

    AudioSource m_AudioSource;
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
    public enum ENEMYTYPE
    {
        GUARD = 0,
        SWAT = 1,
        BOSS = 2
    }
    /// <summary>
    /// Returns the enemy type (guard, swat or boss)
    /// </summary>
    /// <returns></returns>
    public ENEMYTYPE GetEnemyType()
    {
        return m_EnemyType;
    }
    /// <summary>
    /// Starts the healing couroutine 
    /// </summary>
    /// <param name="_amount"></param>
    public void Heal(float _amount)
    {
        if (!m_Healing)
        {
            StartCoroutine(HealRoutine(_amount));
        }
    }
    /// <summary>
    /// Returns the alert speed of the enmy depending on if there in combat or not
    /// </summary>
    /// <returns></returns>
    public float GetAlertSpeed()
    {
        if (m_InCombat)
            return m_AlertDecay_s;
        else
            return m_AlertSpeed_s;
    }
    /// <summary>
    /// Sets the alert level of the enemy to max and updates the last known player location to the blackboard / enemymanager
    /// </summary>
    public void SetAlertMax()
    {
        if (m_Player)
            m_Manager.SetLastKnownLocation(m_Player.position);
        m_AlertLevel = m_AlertDecay_s;
        m_InCombat = true;
    }
    /// <summary>
    /// Sets the waypoints to specified array of transforms
    /// </summary>
    /// <param name="_waypoints"></param>
    public void SetWaypoints(Transform[] _waypoints)
    {
        m_WayPoints = _waypoints;
    }
    /// <summary>
    /// Returns a refrence to the enemy manager
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Checks if the enemy health is low with some random leway +- 20 hp from maxhealth / 2
    /// </summary>
    /// <returns></returns>
    public bool IsHealthLow()
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
    /// <summary>
    /// Increases the enemies alert level by one tick. If its more than the alert speed then set is comabt to true. 
    /// </summary>
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
    /// <summary>
    /// Checks if the enemy should be dead. If hes not dead, play the hit sound
    /// </summary>
    public void CheckForDeath()
    {
        if (m_Health <= 0)
        {
            m_Manager.RemoveEnemy(this);
            Destroy(gameObject);
        }
        else
        {
            m_AudioSource.PlayOneShot(m_HitSound);
        }
    }
    /// <summary>
    /// Decreases the enemy alert by one tick. If its less or equal to 0 then set in combat to false.
    /// </summary>
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
    /// <summary>
    /// Updates the direction from the enemy to the player
    /// </summary>
    /// <param name="_direction"></param>
    public void SetDirectionToPlayer(Vector3 _direction)
    {
        m_DirectionToPlayer = _direction;
    }
    /// <summary>
    /// Returns the player transfrom and updates the direction from the enemy to the player
    /// </summary>
    /// <returns></returns>
    public Transform GetPlayerCache()
    {
        m_DirectionToPlayer = (m_Player.position - (transform.position + Vector3.up * 0.5f)).normalized;
        return m_Player;
    }
    #endregion

    #region Protected
    /// <summary>
    /// Sets up the behavior tree based on enemy type
    /// </summary>
    /// <returns></returns>
    protected override BehaviorNode SetupTree()
    {
        // Grab scripts and values 
        m_AudioSource = GetComponent<AudioSource>();    
        m_Health = m_MaxHealth;
        m_Manager = GameObject.FindWithTag("EnemyManager").GetComponent<Script_EnemyManager>();
        m_Alarm = GameObject.FindWithTag("Alarm").GetComponent<Script_Alarm>();
        m_AttachedAgent = GetComponent<NavMeshAgent>();
        m_HealthStation = GameObject.FindGameObjectWithTag("HealthStation").transform;
        GameObject player = GameObject.FindWithTag("Player");
        if (player)
            m_Player = player.transform;
        
        // Seetup behavior tree based on type
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
            case ENEMYTYPE.BOSS:
                {
                    return BossTreeSetup();
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
        // If bullet hits enemy and it is friendly / from player then take damage
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
        // Used in case enter failed to get called
        // If bullet hits enemy and it is friendly / from player then take damage
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
    /// <summary>
    /// Handles the enemy taking damage and prevents the enmies health from going below 0
    /// Also updates the random selection value for the fleeing of an enemy every time the enemy gets hit. This is to prevent all enemies from having the same random selection
    /// </summary>
    /// <param name="_amount"></param>
    /// <returns></returns>
    IEnumerator DamageRoutine(float _amount)
    {
        m_TakingDamage = true;

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
                break;
            }
        }
        CheckForDeath();
        yield return new WaitForSeconds(m_DamageInterval_s);
        m_TakingDamage = false;
    }
    // Heals the enemy and prevents its health from going past its maxHP
    // Keeps thread open until healing is complete using a delegate WaitUntil
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
    /// <summary>
    /// Sets up the behvaior tree for a guard
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Seets up the behavior tree for a swat
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Sets up the behavior tree for a boss
    /// </summary>
    /// <returns></returns>
    BehaviorNode BossTreeSetup()
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
