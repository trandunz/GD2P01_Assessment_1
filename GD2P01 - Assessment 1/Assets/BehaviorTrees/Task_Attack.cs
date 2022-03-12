using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Attack : BehaviorNode
{
    #region Member Variables
    Script_Enemy m_EnemyScript;
    Transform m_Transform;
    Transform m_Target;
    #endregion

    #region Public
    public Task_Attack(Script_Enemy _enemyScript, Transform _target)
    {
        m_EnemyScript = _enemyScript;
        m_Transform = m_EnemyScript.transform;
        m_Target = _target;
    }
    public override BehaviorNodeState Evaluate()
    {
        m_Transform.LookAt(new Vector3(m_Target.position.x, m_Transform.position.y, m_Target.position.z));
        m_EnemyScript.FireBullet();
        p_State = BehaviorNodeState.RUNNING;

        return p_State;
    }
    #endregion
}
