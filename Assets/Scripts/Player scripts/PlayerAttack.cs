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

    public float meleeAttackDistance = 4f;
    public float meleeAttackDelay = 0.3f;
    public float meleeAttackSpeed = 0.4f;
    public int meleeAttackDamage = 2;

    public float rangeAttackDistance = 50f;
    public float rangeAttackDelay = 2f;
    public float rangeAttackSpeed = 2f;
    public int rangeAttackDamage = 2;

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

    public void OnFire(InputValue btn) {
        if (btn.isPressed)
        {
            RangeAttack();
        }
    }

    public void OnMelee(InputValue btn) {
        if (btn.isPressed)
        {
            MeleeAttack();
        }
    }

    public void RangeAttack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), rangeAttackSpeed);

        // Run Animation
        RangeAnimation();
    }

    public void MeleeAttack() {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), meleeAttackSpeed);
        Invoke(nameof(AttackRaycast), meleeAttackDelay);

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
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, meleeAttackDistance, enemyLayer))
        {
            HitTarget();

            // Destroy enemy
            if (hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth T))
            {
                T.TakeDamage(meleeAttackDamage);
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

    private void RangeAnimation()
    {
        leftArmAnim.SetTrigger("ShootTrigger");
        rightArmAnim.SetTrigger("ShootTrigger");
    }
}
