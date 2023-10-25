using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public float damageDelay = 0.8f;
    public float damageDuration = 0.4f;

    public TextMeshProUGUI looseText;

    int currentHealth;
    int currentDamageAmount;
    bool beingDamaged = false;
    bool canBeDamaged = true;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentDamageAmount = 0;
        looseText.gameObject.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (!canBeDamaged || beingDamaged) return;

        canBeDamaged = false;
        beingDamaged = true;
        currentDamageAmount = amount;

        Invoke(nameof(ResetTakeDamage), damageDelay);
        Invoke(nameof(DealDamage), damageDuration);
        Debug.Log("Player is taking Damage :(");
    }

    private void DealDamage()
    {
        currentHealth -= currentDamageAmount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void ResetTakeDamage() {
        beingDamaged = false;
        canBeDamaged = true;
        currentDamageAmount = 0;
    }

    private void Death() {
        Debug.Log("Player is now a ghost");
        looseText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
