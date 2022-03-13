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
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void SwatTeamMessage()
    {
        m_Image.sprite = m_Images[0];
        m_TextMeshPro.text = "Swat Team Incoming : 5 Seconds";
        m_Animator.SetTrigger("Popup");
    }
}
