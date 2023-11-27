using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// The different states of the day/night cycle
public enum TimeCycle
{
    DAWN,
    DAY,
    DUSK,
    NIGHT
};

// This script calculates whether its day or night based on the elapsed timer
public class InGameTimer : MonoBehaviour
{
    [SerializeField] private ElapsedTimer et; // Gets the elapsed timer from ElapsedTimer.cs
    [SerializeField] private TMP_Text inGameTimer_Text;
    [SerializeField] private bool isDone;
    public int count; // Needed to count how many days have passed
    public bool gameFinished; // Needed to check if game is over or still running
    public TimeCycle timeCycle;
    public GameObject sky;
    Animator cloudAnim; // Needed to activate the on / off cloud animations

    void Start()
    {
        inGameTimer_Text = GetComponent<TextMeshProUGUI>(); // Accesses the text in the UI
        cloudAnim = sky.GetComponent<Animator>(); // Accesses the cloud animator
        inGameTimer_Text.enabled = true;
        count = 0; // Resets the counter
        isDone = false;
        gameFinished = false;
        timeCycle = TimeCycle.DAY;
    }

    void Update()
    {
        float eTime = Mathf.Round(et.elapsedTime); // Rounds the elapsed time to the nearest second

        if (eTime == 520 || eTime == 1240 || eTime == 1960 || eTime == 2680 || eTime == 3400) // Every few minutes, when game state is "dawn"
        {
            timeCycle = TimeCycle.DAWN;
            inGameTimer_Text.text = "In Game Time: Dawn";
            while (!isDone) // After each day has begun (dawn), increments the count (only once each day, then stops incrementing)
            {
                count++;
                isDone = true;
            }
            cloudAnim.SetTrigger("DawnTrigger"); // Trigger the cloud "on" animation
        }

        if (eTime == 0 || eTime == 550 || eTime == 1270 || eTime == 1990 || eTime == 2710) // Every few minutes, when game state is "day"
        {
            timeCycle = TimeCycle.DAY;
            inGameTimer_Text.text = "In Game Time: Day";
            isDone = false; // Resets isDone variable, so next dawn can trigger another count increment
        }

        if (eTime == 170 || eTime == 890 || eTime == 1610 || eTime == 2330 || eTime == 3050) // Every few minutes, when game state is "dusk"
        {
            timeCycle = TimeCycle.DUSK;
            inGameTimer_Text.text = "In Game Time: Dusk";
            isDone = false;
            cloudAnim.SetTrigger("DuskTrigger"); // Trigger the cloud "off" animation
        }

        if (eTime == 200 || eTime == 920 || eTime == 1640 || eTime == 2360 || eTime == 3080) // Every few minutes, when game state is "night"
        {
            timeCycle = TimeCycle.NIGHT;
            inGameTimer_Text.text = "In Game Time: Night";
            isDone = false;
        }

        if (eTime >= 3430) // If 5th night/dawn has passed, end game
        {
            gameFinished = true;
            inGameTimer_Text.enabled = false;
        }

    }
}
