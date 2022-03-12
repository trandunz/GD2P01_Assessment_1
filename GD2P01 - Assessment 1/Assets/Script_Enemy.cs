using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Script_Enemy : TaskBehaviorTree
{
    #region Member Variables
    [SerializeField] static float m_MaxHealth = 100.0f;
    [SerializeField] Script_Alarm m_Alarm;
    [SerializeField] float m_VisionDistance = 10.0f, m_DamageInterval_s = 0.2f;
    [SerializeField] Transform[] m_WayPoints;
    [SerializeField] GameObject m_BulletPrefab;
    [SerializeField] Transform m_Player;
    public bool m_SeenPlayer = false, m_OnRoute = false;
    public Vector3 m_DirectionToPlayer;
    [SerializeField] float m_Health = m_MaxHealth;
    bool m_TakingDamage = false;
    #endregion

    #region Public
    public bool HealthLow()
    {
        if (m_Health < 50.0f)
        {
            return true;
        }
        return false;
    }
    public void FireBullet()
    {
        GameObject bullet = Instantiate(m_BulletPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        bullet.GetComponent<Script_Bullet>().SetDirection(m_DirectionToPlayer);
    }
    #endregion

    #region Protected
    protected override BehaviorNode SetupTree()
    {
        BehaviorNode rootNode = new BehaviorSelector(new List<BehaviorNode>
        {
            new BehaviorSequence(new List<BehaviorNode>
            {
                new Check_TargetVisible(this, GetComponent<NavMeshAgent>(), m_Player, m_VisionDistance),
                new BehaviorSelector(new List<BehaviorNode>
                {
                    new BehaviorWaitUntil
                    (
                        new BehaviorSequence(new List<BehaviorNode>
                        {
                            new Check_AlarmCalled(m_Alarm),
                            new BehaviorSelector(new List<BehaviorNode>
                            {
                                new BehaviorSequence(new List<BehaviorNode>
                                {
                                    new Check_HPLow(this),
                                    new Task_Flee(GetComponent<NavMeshAgent>(), m_WayPoints[0], m_Player)
                                }),
                                new Task_Attack(this, m_Player)
                            })
                        })
                    ),
                    new BehaviorSequence(new List<BehaviorNode>
                    {
                        new Check_GuardOnWay(m_Alarm, this),
                        new BehaviorSelector(new List<BehaviorNode>
                        {
                            new BehaviorSequence(new List<BehaviorNode>
                            {
                                new Check_HPLow(this),
                                new Task_Flee(GetComponent<NavMeshAgent>(), m_WayPoints[0], m_Player)
                            }),
                            new Task_Attack(this, m_Player)
                        })
                    }),
                    new Task_CallAlarm(GetComponent<NavMeshAgent>(), m_Alarm, this)
                })
            }),
            new Task_Patrol(GetComponent<NavMeshAgent>(), m_WayPoints)
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
        for(int i = 0; i < _amount; _amount--)
        {
            if (m_Health > 0)
            {
                m_Health--;
            }
            else
            {
                yield return null;
            }
        }
        yield return new WaitForSeconds(m_DamageInterval_s);
        m_TakingDamage = false;
    }
    #endregion
}
