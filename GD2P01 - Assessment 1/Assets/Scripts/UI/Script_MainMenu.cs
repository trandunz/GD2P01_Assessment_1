using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_MainMenu : MonoBehaviour
{
    #region Public
    public void StartNewGameNoTutorial()
    {
        SceneManager.LoadScene(2);
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
