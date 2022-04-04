// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_Interactable.cs 
// Description : Animated and Openable Interactable Objects Implementation File
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Interactable : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] float m_InteractTime = 0.5f;
    float m_InteractionTimer = 0.0f;
    bool m_Interacting = false;
    Animator m_Animator = null;
    AudioSource m_Audio = null;
    #endregion

    #region Private
    void Start()
    {
        // Grab and assign values
        m_InteractionTimer = m_InteractTime;
        m_Animator = GetComponent<Animator>();
        m_Audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        // If object is interacting then decrease timer. If timer reaches 0, interaction has finished
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
    #endregion

    #region Public
    /// <summary>
    /// Calls the animator bool "open" if an animator is available and sets interating to true which begins interaction timer in update.
    /// </summary>
    public void Interact()
    {
        m_Interacting = true;
        if (m_Animator != null)
        {
            m_Animator.SetBool("Open", true);
        }
        
    }
    public void PlayOpenAudioIfAvailable()
    {
        if (m_Audio != null)
        {
            m_Audio.Play();
        }
    }
    public void PlayCloseAudioIfAvailable()
    {
        if (m_Audio != null)
        {
            m_Audio.Play();
        }
    }
    public bool HasInteractFinished()
    {
        return !m_Interacting;
    }
    #endregion
}
