using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_PlayerUI : MonoBehaviour
{
    [SerializeField] Image m_HealthBar;
    Script_Player m_PlayerScript;

    private void Start()
    {
        m_PlayerScript = GetComponent<Script_Player>();
    }
    private void Update()
    {
        m_HealthBar.fillAmount = m_PlayerScript.GetHealth() / m_PlayerScript.GetMaxHealth();
    }
}
