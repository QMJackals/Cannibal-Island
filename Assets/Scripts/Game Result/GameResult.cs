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
            gameOverLabel.enabled = false;
            winLabel.enabled = true;
        } else
        {
            gameOverLabel.enabled = true;
            winLabel.enabled = false;
        }
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
