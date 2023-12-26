using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 1;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth P)) {
                P.TakeDamage(attackDamage);
            }
        }
    }
}
