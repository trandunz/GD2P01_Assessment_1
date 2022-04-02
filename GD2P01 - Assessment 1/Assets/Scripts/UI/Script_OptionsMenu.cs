using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_OptionsMenu : MonoBehaviour
{
    #region MemberVariables
    [SerializeField] Dropdown m_ResolutionDropDown;
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
