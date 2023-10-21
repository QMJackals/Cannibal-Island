using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This script calculates whether its day or night based on the elapsed timer
public class InGameTimer : MonoBehaviour
{
    [SerializeField] private ElapsedTimer et; // Gets the elapsed timer from ElapsedTimer.cs
    [SerializeField] private TMP_Text inGameTimer_Text;
    [SerializeField] private bool isDone;
    public int count; // Needed to count how many days have passed
    void Start()
    {
        inGameTimer_Text = GetComponent<TextMeshProUGUI>(); // Accesses the text in the UI
        count = -1; // Resets the counter
        isDone = false;
    }
    void Update()
    {
        float eTime = Mathf.Round(et.elapsedTime); // Rounds the elapsed time to the nearest second
        if (eTime % 10 == 0) // Every 10 seconds, display the game time as "Day"
        {
            inGameTimer_Text.text = "In Game Time: Day";
            while (!isDone) // After each day has begun, increments the count (only once each day, then stops incrementing)
            {
                count++;
                isDone = true;
            }
        }
        if (eTime % 5 == 0 && eTime % 10 != 0) // Otherwise, display the game time as "Night"
        {
            inGameTimer_Text.text = "In Game Time: Night";
            isDone = false; // Resets isDone variable, so next day can trigger another count increment
        }
    }
}
