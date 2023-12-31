using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{

    public float mouseSentivityDefault = 500f;
    public float mouseSentivity;

    //a reference from the main camera to our entire first person player object so that we can rotate around
    public Transform playerBody;

    float xRotation = 0f;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSentivity = PlayerPrefs.GetFloat("currentSensitivity", mouseSentivityDefault);
        slider.value = mouseSentivity / 10;
    }

    // Update is called once per frame
    void Update()
    {
        //Time.deltatime is the amount of time that has gone by since the last time the update function was called
        float mouseX = Input.GetAxis("Mouse X") * mouseSentivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSentivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void Adjustspeed(float newSpeed)
    {
        mouseSentivity = newSpeed * 10;
    }
}
