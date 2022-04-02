using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_PlayerUI : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] Image m_HealthBar;
    Script_Player m_PlayerScript;
    #endregion

    #region Private
    void Start()
    {
        m_PlayerScript = GetComponent<Script_Player>();
    }
    void Update()
    {
        m_HealthBar.fillAmount = m_PlayerScript.GetHealth() / m_PlayerScript.GetMaxHealth();
    }
    #endregion
}
