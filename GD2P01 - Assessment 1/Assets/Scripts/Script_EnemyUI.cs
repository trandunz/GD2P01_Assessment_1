using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_EnemyUI : MonoBehaviour
{
    [SerializeField] Image m_HealthBar;
    [SerializeField] Image m_DetectionBar;
    Script_Enemy m_EnemyScript;

    private void Start()
    {
        m_EnemyScript = GetComponent<Script_Enemy>();
    }
    private void Update()
    {
        m_HealthBar.fillAmount = m_EnemyScript.GetHealth() / m_EnemyScript.GetMaxHealth();
        m_DetectionBar.fillAmount = m_EnemyScript.GetAlertLevel() / m_EnemyScript.GetAlertSpeed();
    }
}
