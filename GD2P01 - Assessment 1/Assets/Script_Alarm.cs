using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Alarm : MonoBehaviour
{
    [SerializeField] bool m_Activated = false;
    bool m_GuardOnWay = false;

    public bool IsActivated()
    {
        return m_Activated;
    }
    public bool IsGuardOnWay()
    {
        return m_GuardOnWay;
    }
    public void Trigger()
    {
        m_Activated = true;
    }
    public void SetGuardOnWay()
    {
        m_GuardOnWay = true;
    }
}
