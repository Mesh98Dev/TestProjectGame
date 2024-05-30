using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Method to start the game
    public void StartGame()
    {
        SceneManager.LoadScene("SDAMap");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}


