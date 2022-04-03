using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_EnemyUI : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] Image m_HealthBar;
    [SerializeField] Image m_DetectionBar;
    Script_Enemy m_EnemyScript;
    #endregion

    #region Private
    void Start()
    {
        // Grab enemy script
        m_EnemyScript = GetComponent<Script_Enemy>();
    }
    void Update()
    {
        // Constantly update health and detection UI meters in corespondance to the values
        m_HealthBar.fillAmount = m_EnemyScript.GetHealth() / m_EnemyScript.GetMaxHealth();
        m_DetectionBar.fillAmount = m_EnemyScript.GetAlertLevel() / m_EnemyScript.GetAlertSpeed();
    }
    #endregion
}
