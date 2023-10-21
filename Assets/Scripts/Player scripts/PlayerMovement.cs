using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;  //https://www.youtube.com/watch?v=_QajrabyTJc

    public float speed = 12f;
    public float gravity = -9.81f;

    //referece for ground check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    //it is used to control what object this sphere should check for
    //we dont want it to register as standing on the ground just because
    //it collide with the player
    public LayerMask groundMask;

    bool isGrounded;

    //it is ued to store our current velocity
    Vector3 velocity;


    // Update is called once per frame
    void Update()
    {
        //https://www.youtube.com/watch?v=_QajrabyTJc
        //this's going ti create a sphere based on the ground check
        //if the invisible sphere under the player collides with anything that is in our ground mask,
        //so it is grounded. Thus, isGrounded is going to be True.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        //https://www.youtube.com/watch?v=_QajrabyTJc
        //these inputs will be turned into a direction that we want to move
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //this is like an arrow that points in the direction that we want to move
        //we dont use new Vector3(x, 0f, z) because this would be global coordinates
        //so we would always move in the same direction no matter what way our player is facing
        //transform.right takes the direction that the player is facing and then goes to the right
        //transform.left is just the same
        Vector3 move = transform.right * x + transform.forward * z;

        //speed is used to control speed of player movement
        //Time.deltatime makes it frame rate dependent
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //up and down axis
        //increase velocity by some gravity
        velocity.y += gravity * Time.deltaTime;

        //in order to add this velocity to our player, we can use
        controller.Move(velocity * Time.deltaTime);
    }
}
