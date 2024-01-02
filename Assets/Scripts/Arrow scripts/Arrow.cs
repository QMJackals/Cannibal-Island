using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Code is inspired by https://www.youtube.com/watch?v=Fu9X3OowEy0
    // Properties for making the arrow fly
    public float torque;
    public Rigidbody rb;
    bool isFlying;
    bool didHit;

    // Properties for checking if the arrow has hit the ground yet
    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;
    bool isGrounded = false;

    // Properties for dealing damage to enemy
    int damage = 8;
    string enemyTag = "Enemy";

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
    
    public void Fly(Vector3 force) {
        isFlying = true;
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        // Prevent arrow from flying below terrain
        if (isFlying && !didHit && !isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded)
            {
                arrowCollision();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (didHit || !isFlying) return;
        arrowCollision();

        Debug.Log("triggered by: " + other.name);
        transform.SetParent(other.transform);

        if (other.CompareTag(enemyTag))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        // Prevent bug of arrow sticking to the player
        else if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Stops arrow from flying after colliding with another game object
    void arrowCollision()
    {
        didHit = true;
        isFlying = false;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

}
