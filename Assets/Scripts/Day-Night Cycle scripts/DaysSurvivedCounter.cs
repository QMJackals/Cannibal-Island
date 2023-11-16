using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaysSurvivedCounter : MonoBehaviour
{
    [SerializeField] private InGameTimer igt; // Gets the in game timer from InGameTimer.cs
    [SerializeField] private TMP_Text daysSurvivedCounter_Text;
    void Start()
    {
        daysSurvivedCounter_Text = GetComponent<TextMeshProUGUI>(); // Accesses the text in the UI
        daysSurvivedCounter_Text.enabled = true;
    }
    void Update()
    {
        if (igt.gameFinished == false) // The game is running
        {
            int numDaysSurvived = igt.count; // Gets the number of days survived
            daysSurvivedCounter_Text.text = "Days Survived: " + numDaysSurvived; // UI text displays the number of days survived
        }
        else // The game has finished
        {
            daysSurvivedCounter_Text.enabled = false;
        }
    }
}
