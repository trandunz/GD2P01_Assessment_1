using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_DialoguePopup : MonoBehaviour
{
    [SerializeField] Image m_Image;
    [SerializeField] TMPro.TextMeshProUGUI m_TextMeshPro;
    [SerializeField] Sprite[] m_Images;
    Animator m_Animator;

    public void SwatTeamMessage()
    {
        m_Image.sprite = m_Images[0];
        m_TextMeshPro.text = "Swat Team Incoming : 5 Seconds";
        m_Animator.SetTrigger("Popup");
    }
    public void EnemyFoundMessage()
    {
        m_Image.sprite = m_Images[1];
        m_TextMeshPro.text = "Hes Over Here! Sound The Alarm";
        m_Animator.SetTrigger("Popup");
    }

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

}
