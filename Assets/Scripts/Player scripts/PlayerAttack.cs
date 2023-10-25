using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Code was inspired by https://www.youtube.com/watch?v=yZhKUViKS_w
    public Camera cam;

    public float attackDistance = 5f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask enemyLayer;

    bool attacking = false;
    bool readyToAttack = true;

    public void OnFire(InputValue btn) {
        if (btn.isPressed)
        {
            Attack();
        }
    }


    public void Attack() {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);
        Debug.Log("Player Attack!");
    }

    private void ResetAttack()
    {
        readyToAttack = true;
        attacking = false;
    }

    private void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, enemyLayer))
        {
            HitTarget();

            // Destroy enemy
            if (hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth T))
            {
                T.TakeDamage(attackDamage);
            }
        }
    }

    private void HitTarget() {
        // Used later
        Debug.Log("Target hit!");
    }

}
