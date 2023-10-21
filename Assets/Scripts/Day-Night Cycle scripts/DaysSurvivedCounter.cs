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
    }
    void Update()
    {
        int numDaysSurvived = igt.count; // Gets the number of days survived
        daysSurvivedCounter_Text.text = "Days Survived: " + numDaysSurvived; // UI text displays the number of days survived
        if (numDaysSurvived == 5)
        {
            // Game is done
        }
    }
}
