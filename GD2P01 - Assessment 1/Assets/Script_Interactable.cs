using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Interactable : MonoBehaviour
{
    [SerializeField] float m_InteractTime = 0.5f;
    float m_InteractionTimer = 0.0f;
    bool m_Interacting = false;
    Animator Animator = null;
    void Start()
    {
        m_InteractionTimer = m_InteractTime;
        Animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (m_Interacting)
        {
            if (m_InteractTime <= 0)
            {
                m_InteractTime = m_InteractionTimer;
                m_Interacting = false;
            }
            else
                m_InteractTime -= Time.deltaTime;
        }
    }
    public void Interact()
    {
        m_Interacting = true;
        if (Animator != null)
        {
            Animator.SetBool("Open", true);
        }
    }
    public bool HasInteractFinished()
    {
        return !m_Interacting;
    }
}
