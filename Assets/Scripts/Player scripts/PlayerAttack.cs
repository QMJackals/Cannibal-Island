using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Code was inspired by https://www.youtube.com/watch?v=yZhKUViKS_w
    public Camera cam;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject armWithKnife;

    public float attackDistance = 4f;
    public float attackDelay = 0.3f;
    public float attackSpeed = 0.4f;
    public int attackDamage = 2;
    public LayerMask enemyLayer;

    bool attacking = false;
    bool readyToAttack = true;

    Animator leftArmAnim;
    Animator rightArmAnim;
    Animator armWithKnifeAnim;

    private void Start()
    {
        leftArmAnim = leftArm.GetComponent<Animator>();
        rightArmAnim = rightArm.GetComponent<Animator>();
        armWithKnifeAnim = armWithKnife.GetComponent<Animator>();
    }

    public void OnMelee(InputValue btn) {
        if (btn.isPressed)
        {
            MeleeAttack();
        }
    }

    public void MeleeAttack() {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        // Run animation
        MeleeAnimation();
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

    private void MeleeAnimation()
    {
        leftArmAnim.SetTrigger("StabTrigger");
        armWithKnifeAnim.SetTrigger("StabTrigger");
    }
}
