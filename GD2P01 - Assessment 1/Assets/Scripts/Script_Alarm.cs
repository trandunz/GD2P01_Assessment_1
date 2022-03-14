using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Alarm : MonoBehaviour
{
    [SerializeField] bool m_Activated = false;
    Script_Reinforcements Script_ReinforcementVan;
    Script_Enemy m_GuardOnWay = null;
    Script_DialoguePopup m_DialoguePopupHandler;
    private void Start()
    {
        m_DialoguePopupHandler = GameObject.FindWithTag("DialoguePopup").GetComponent<Script_DialoguePopup>();
        Script_ReinforcementVan = GameObject.FindWithTag("Reinforcements").GetComponent<Script_Reinforcements>();
    }

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
        StartCoroutine(CallReinforcementsRoutine());
        m_Activated = true;
        GetComponent<AudioSource>().Play();
        m_DialoguePopupHandler.SwatTeamMessage();
    }
    public void SetGuardOnWay(Script_Enemy _guard)
    {
        m_GuardOnWay = _guard;
    }

    IEnumerator CallReinforcementsRoutine()
    {
        yield return new WaitForSeconds(5);
        Script_ReinforcementVan.PullUp();
    }
}
