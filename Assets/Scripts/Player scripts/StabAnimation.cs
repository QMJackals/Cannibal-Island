using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is inspired by the following tutorial: https://www.youtube.com/watch?v=gQI2ZjCK2RA&t
// It triggers the simple knife attack animation upon left mouse click
public class StabAnimation : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>(); // Gets the animator component
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("StabTrigger"); // Upon left mouse click, trigger the stab animation
        }
    }
}
