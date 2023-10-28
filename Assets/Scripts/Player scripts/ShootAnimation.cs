using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is inspired by the following tutorial: https://www.youtube.com/watch?v=gQI2ZjCK2RA&t
// It triggers the simple bow shoot animation upon right mouse click
public class ShootAnimation : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>(); // Gets the animator component
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("ShootTrigger"); // Upon right mouse click, trigger the shoot animation
        }
    }
}
