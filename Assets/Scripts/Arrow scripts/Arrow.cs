using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Code is inspired by https://www.youtube.com/watch?v=Fu9X3OowEy0
    public float torque;
    public Rigidbody rb;

    int damage = 1;
    string enemyTag = "Enemy";
    bool isFlying;
    bool didHit;

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

    private void Update()
    {
        // Prevent arrow from flying below terrain
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (didHit || !isFlying) return;
        didHit = true;
        isFlying = false;

        Debug.Log("triggered by: " + other.name);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
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
}
