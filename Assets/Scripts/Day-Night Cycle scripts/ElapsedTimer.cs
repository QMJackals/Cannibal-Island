using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This script keeps track of the elapsed time of the game running (in seconds)
public class ElapsedTimer : MonoBehaviour
{
    public float elapsedTime = 0f; // Initialise timer to 0
    [SerializeField] private TMP_Text elapsedTimer_Text;
    void Start()
    {
        elapsedTimer_Text = GetComponent<TextMeshProUGUI>(); // Accesses the text in the UI
    }
    void Update()
    {
        elapsedTime += Time.deltaTime; // Every frame, the elapsed time is calculated
        elapsedTimer_Text.text = "Elapsed Time: " + Mathf.Round(elapsedTime); // Changes the UI text to display the elapsed time (rounded up to nearest second)
    }
}
