using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningUI : MonoBehaviour
{
    public GameObject winningCanvas;
    public GameObject crosshair;
    
    void Start()
    {
        // Ensure the winning canvas is inactive at the start
        winningCanvas.SetActive(false);
    }

    public void ShowWinningUI()
    {
        gameObject.SetActive(true);
        StartCoroutine(DisplayWinningUI());
    }

    private IEnumerator DisplayWinningUI()
    {
        // Activate the winning canvas
        crosshair.SetActive(false);
        winningCanvas.SetActive(true);

        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Reload the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
