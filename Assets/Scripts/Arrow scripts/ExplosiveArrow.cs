using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : MonoBehaviour
{
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
    int damage = 2;
    string enemyTag = "Enemy";
    int blastRadius = 10;

    // Explosion Partical System
    public ParticleSystem explosionPrefab;
    
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public void Fly(Vector3 force)
    {
        isFlying = true;
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        // Check if arrow hit the ground and explode upon impact
        if (isFlying && !didHit && !isGrounded)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded)
            {
                arrowCollision();
                arrowExplode();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (didHit || !isFlying) return;
        arrowCollision();

        // Destroy arrow
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else
        {
            arrowExplode();
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

    // Damage enemies in the explosion
    void arrowExplode()
    {
        // Set origin of explosion to be arrow's position
        Vector3 explosionOrigin = transform.position;
        // Trigger explode effect
        Instantiate(explosionPrefab, explosionOrigin, Quaternion.identity);
        // Get Enemies in the explosion
        List<EnemyHealth> enemiesInExplosion = GetEnemiesInBlastRadius(explosionOrigin);
        // Damage the enemies after 0.2 second delay
        StartCoroutine(DamageEnemies(enemiesInExplosion));
    }

    // Wait 0.2 seconds before dealing damage to enemies and then destroy arrow
    IEnumerator DamageEnemies(List<EnemyHealth> enemies)
    {
        yield return new WaitForSeconds(0.2f);
        // Deal damage to enemies
        foreach (EnemyHealth enemy in enemies)
        {
            enemy.TakeDamage(damage);
        }
        // Destroy the arrow after killing the enemies
        Destroy(gameObject);
    }

    List<EnemyHealth> GetEnemiesInBlastRadius(Vector3 explosionOrigin)
    {
        // Find all nearby colliders based on the explosion origin
        Collider[] nearbyColliders = Physics.OverlapSphere(explosionOrigin, blastRadius);
        List<EnemyHealth> enemies = new List<EnemyHealth>();

        // Get the EnemyHealth component of enemies
        foreach (Collider col in nearbyColliders)
        {
            if (col.CompareTag(enemyTag))
            {
                enemies.Add(col.GetComponent<EnemyHealth>());
            }
        }
        return enemies;
    } 
}
