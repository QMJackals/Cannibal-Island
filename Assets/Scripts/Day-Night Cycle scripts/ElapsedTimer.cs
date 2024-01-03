using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This script keeps track of the elapsed time of the game running (in seconds)
public class ElapsedTimer : MonoBehaviour
{
    public float elapsedTime = 0f; // Initialise timer to 0
    [SerializeField] private TMP_Text elapsedTimer_Text;
    [SerializeField] private InGameTimer igt; // Gets the in game timer from InGameTimer.cs
    void Start()
    {
        elapsedTimer_Text = GetComponent<TextMeshProUGUI>(); // Accesses the text in the UI
        elapsedTimer_Text.enabled = true;
    }
    void Update()
    {
        if (igt.gameFinished == false) // The game is running
        {
            elapsedTime += Time.deltaTime * 4; // Every frame, the elapsed time is calculated
            elapsedTimer_Text.text = "Elapsed Time: " + Mathf.Round(elapsedTime); // Changes the UI text to display the elapsed time (rounded up to nearest second)
        }
        else // The game has finished
        {
            elapsedTimer_Text.enabled = false;
        }        
    }
}
