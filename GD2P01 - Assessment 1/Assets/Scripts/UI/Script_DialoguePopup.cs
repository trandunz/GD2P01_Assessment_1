using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_DialoguePopup : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] bool m_Tutorial = false;
    [SerializeField] Image m_Image;
    [SerializeField] TMPro.TextMeshProUGUI m_TextMeshPro;
    [SerializeField] Sprite[] m_Images;
    Animator m_Animator;
    bool m_EnemyFound = false;
    bool m_SwatTeamArriving = false;
    #endregion

    #region Private
    void Start()
    {
        // grab animator component
        m_Animator = GetComponent<Animator>();

        // On Game Start, play a message
        if (m_Tutorial)
        {
            TutorialMessage();
        }
        else
        {
            MissionStartMessage();
        }
    }
    #endregion

    #region Public
    public void BossMessageTutorial()
    {
        m_Image.sprite = m_Images[2];
        m_TextMeshPro.text = "Alright Magot. We Placed A Robot Target Ahead. Take Him Out And Escape Through The Vent.";
        m_Animator.speed = 0.5f;
        m_Animator.SetTrigger("Popup");
    }
    public void DetectionMessageTutorial()
    {
        m_Image.sprite = m_Images[2];
        m_TextMeshPro.text = "Guards Have Vision! Duck And Roll To Avoid Detection.";
        m_Animator.SetTrigger("Popup");
    }
    public void VentMessageTutorial()
    {
        m_Image.sprite = m_Images[2];
        m_TextMeshPro.text = "You Can Use Vents To Avoid Trouble.";
        m_Animator.SetTrigger("Popup");
    }
    public void SwatTeamMessage()
    {
        if (!m_SwatTeamArriving)
        {
            m_SwatTeamArriving = true;
            m_Image.sprite = m_Images[0];
            m_TextMeshPro.text = "Swat Team Incoming : 5 Seconds";
            m_Animator.SetTrigger("Popup");
        }
    }
    public void EnemyFoundMessage()
    {
        Debug.Log("Seen Enemy For First Time!");
        if (!m_EnemyFound)
        {
            m_EnemyFound = true;
            m_Image.sprite = m_Images[1];
            m_TextMeshPro.text = "Theres Someone Here! Sound The Alarm";
            m_Animator.SetTrigger("Popup");
        }
    }
    public void TutorialMessage()
    {
        m_Image.sprite = m_Images[2];
        m_TextMeshPro.text = "Alright Private Idiot! This Is Your Chance To Learn The Ropes";
        m_Animator.SetTrigger("Popup");
    }
    public void MissionStartMessage()
    {
        m_Image.sprite = m_Images[2];
        m_TextMeshPro.text = "Alright Soldier, This Is The Real Deal. Locate And Execute The Target.";
        m_Animator.SetTrigger("Popup");
    }
    #endregion
}
