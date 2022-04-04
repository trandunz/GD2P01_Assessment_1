// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_OptionsMenu.cs 
// Description : Handles Options Menu Actions
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_OptionsMenu : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] Dropdown m_ResolutionDropDown;
    #endregion

    #region Private
    void OnEnable()
    {
        // Update drop down menu to make sure it matches current resolution
        switch (Screen.currentResolution.width)
        {
            case 2560:
                {
                    m_ResolutionDropDown.value = 0;
                    break;
                }
            case 1920:
                {
                    m_ResolutionDropDown.value = 1;
                    break;
                }
            case 1280:
                {
                    m_ResolutionDropDown.value = 2;
                    break;
                }
            default:
                break;
        }
    }
    #endregion

    #region Public
    public void SetFullScreen()
    {
        Screen.fullScreen = true;
    }
    public void SetWindowed()
    {
        Screen.fullScreen = false;
    }

    /// <summary>
    /// Called When dropdown menu is changed and sets the resolution to selected value
    /// </summary>
    public void UpdateResolution()
    {
        switch(m_ResolutionDropDown.value)
        {
            case 0:
                {
                    Screen.SetResolution(2560,1440, Screen.fullScreen);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1920,1080, Screen.fullScreen);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(1280,720, Screen.fullScreen);
                    break;
                }
            default:
                break;
        }
    }
    #endregion
}
