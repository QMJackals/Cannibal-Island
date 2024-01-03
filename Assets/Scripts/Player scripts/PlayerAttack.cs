using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    // Code was inspired by https://www.youtube.com/watch?v=yZhKUViKS_w
    public Camera cam;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject armWithKnife;
    public TextMeshProUGUI noRangeAmmoLabel;

    public Transform arrowSpawnPoint;
    public Arrow arrowPrefab;
    public ExplosiveArrow explosiveArrowPrefab;

    public float meleeAttackDistance = 4f;
    public float meleeAttackDelay = 0.3f;
    public float meleeAttackSpeed = 0.4f;
    public int meleeAttackDamage = 2;

    public float rangeAttackDistance = 50f;
    public float rangeAttackSpeed = 2f;
    public int rangeAttackDamage = 1;

    public LayerMask enemyLayer;

    bool attacking = false;
    bool readyToAttack = true;

    Animator leftArmAnim;
    Animator rightArmAnim;
    Animator armWithKnifeAnim;

    Inventory inventory;

    private void Start()
    {
        leftArmAnim = leftArm.GetComponent<Animator>();
        rightArmAnim = rightArm.GetComponent<Animator>();
        armWithKnifeAnim = armWithKnife.GetComponent<Animator>();
        inventory = gameObject.GetComponent<Inventory>();
    }

    public void OnFire(InputValue btn) {
        if (btn.isPressed)
        {
            if (inventory.HasRangeAttackAmmo())
            {
                RangeAttack();
            } else
            {
                StartCoroutine(ShowNoRangeAmmoLabel());
            }
        }
    }

    public void OnMelee(InputValue btn) {
        if (btn.isPressed)
        {
            MeleeAttack();
        }
    }

    public void OnToggleInventory(InputValue btn)
    {
        if (btn.isPressed)
        {
            // Toggle Inventory Current Selection
            inventory.ToggleCurrentSelection();
        }
    }

    public void RangeAttack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        // Run Animation
        RangeAnimation();

        Invoke(nameof(ResetAttack), rangeAttackSpeed);
    }

    // Fire normal arrow
    void RangeAttackFire(Arrow arrow)
    {
        Vector3 force = cam.transform.forward * rangeAttackDistance;
        arrow.Fly(force);
    }

    // Fire explosive arrow
    void RangeAttackFire(ExplosiveArrow arrow)
    {
        Vector3 force = cam.transform.forward * rangeAttackDistance;
        arrow.Fly(force);
    }

    public void MeleeAttack() {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), meleeAttackSpeed);
        Invoke(nameof(MeleeAttackRaycast), meleeAttackDelay);

        // Run animation
        MeleeAnimation();
    }

    private void ResetAttack()
    {
        readyToAttack = true;
        attacking = false;
    }

    private void MeleeAttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, meleeAttackDistance, enemyLayer))
        {
            // Destroy enemy
            if (hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth T))
            {
                T.TakeDamage(meleeAttackDamage);
            }
        }
    }

    private void MeleeAnimation()
    {
        leftArmAnim.SetTrigger("StabTrigger");
        armWithKnifeAnim.SetTrigger("StabTrigger");
    }

    // Called after Arrow animation finishes to spawn a normal arrow or explosive arrowhjm
    public void SpawnRangeAmmo()
    {
        InventoryItemType currentSelection = inventory.GetCurrentSelection();
        if (currentSelection == InventoryItemType.ARROW)
        {
            SpawnArrow();
        } else if (currentSelection == InventoryItemType.EXPLOSIVE_ARROW)
        {
            SpawnExplosiveArrow();
        }
    }

    // Spawns a normal arrow at the arrow spawn point
    private void SpawnArrow()
    {
        Arrow currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint);
        currentArrow.transform.localPosition = Vector3.zero;
        // Set the damage of the arrow
        currentArrow.SetDamage(rangeAttackDamage);
        // Fire the arrow
        RangeAttackFire(currentArrow);
        // Deplete range attack ammo from inventory
        inventory.RemoveItem(InventoryItemType.ARROW, -1);
    }

    // Spawns an explosive arrow at the arrow spawn point
    private void SpawnExplosiveArrow()
    {
        Debug.Log("spawning explosive arrow");
        ExplosiveArrow currentArrow = Instantiate(explosiveArrowPrefab, arrowSpawnPoint);
        currentArrow.transform.localPosition = Vector3.zero;
        // Fire the arrow
        RangeAttackFire(currentArrow);
        // Deplete range attack ammo from inventory
        inventory.RemoveItem(InventoryItemType.EXPLOSIVE_ARROW, -1);
    }

    private void RangeAnimation()
    {
        rightArmAnim.Rebind();
        // Trigger the right animation based on the current selection
        leftArmAnim.SetTrigger("ShootTrigger");
        InventoryItemType currentSelection = inventory.GetCurrentSelection();
        if (currentSelection == InventoryItemType.ARROW)
        {
            rightArmAnim.SetTrigger("ShootTriggerNormal");
        } else if (currentSelection == InventoryItemType.EXPLOSIVE_ARROW)
        {
            rightArmAnim.SetTrigger("ShootTriggerExplosive");
        }
    }

    // Show and hide no range ammo label
    private IEnumerator ShowNoRangeAmmoLabel()
    {
        noRangeAmmoLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        noRangeAmmoLabel.gameObject.SetActive(false);
    }
}
