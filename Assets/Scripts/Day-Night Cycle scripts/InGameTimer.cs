using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This script calculates whether its day or night based on the elapsed timer
public class InGameTimer : MonoBehaviour
{
    [SerializeField] private ElapsedTimer et; // Gets the elapsed timer from ElapsedTimer.cs
    [SerializeField] private TMP_Text inGameTimer_Text;
    void Start()
    {
        inGameTimer_Text = GetComponent<TextMeshProUGUI>(); // Accesses the text in the UI
    }
    void Update()
    {
        float eTime = Mathf.Round(et.elapsedTime); // Rounds the elapsed time to the nearest second
        if (eTime % 10 == 0) // Every 10 seconds, display the game time as "Day"
        {
            inGameTimer_Text.text = "In Game Time: Day";            
        }
        if (eTime % 5 == 0 && eTime % 10 != 0) // Otherwise, display the game time as "Night"
        {
            inGameTimer_Text.text = "In Game Time: Night";            
        }
    }
}
