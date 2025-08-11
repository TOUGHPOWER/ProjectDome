using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadWinMenu()
    {
        SceneManager.LoadScene("WinMenu");
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseMenu");
    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
