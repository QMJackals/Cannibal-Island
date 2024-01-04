using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameResult : MonoBehaviour
{
    public TextMeshProUGUI gameOverLabel;
    public TextMeshProUGUI winLabel;

    public void Setup(bool didSurvive)
    {
        // Determine which label is active
        if (didSurvive)
        {
            gameOverLabel.gameObject.SetActive(false);
            winLabel.gameObject.SetActive(true);
        }
        else
        {
            gameOverLabel.gameObject.SetActive(true);
            winLabel.gameObject.SetActive(false);
        }

        // Show game over screen
        gameObject.SetActive(true);

        // Enable cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause Game Time
        Time.timeScale = 0;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }
}
