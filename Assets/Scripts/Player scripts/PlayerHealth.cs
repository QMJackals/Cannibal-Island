using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float damageDelay = 0.8f;
    public float damageDuration = 0.4f;

    public TextMeshProUGUI loseText;

    int currentHealth;
    int currentDamageAmount;
    bool beingDamaged = false;
    bool canBeDamaged = true;

    HealthBar healthBar;

    private void Awake()
    {
        GameObject healthBarUI = GameObject.FindGameObjectWithTag("HealthBar");
        healthBar = healthBarUI.GetComponent<HealthBar>();

        currentHealth = healthBar.health;
        currentDamageAmount = 0;
        loseText.gameObject.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (!canBeDamaged || beingDamaged) return;

        canBeDamaged = false;
        beingDamaged = true;
        currentDamageAmount = amount;

        Invoke(nameof(ResetTakeDamage), damageDelay);
        Invoke(nameof(DealDamage), damageDuration);
    }

    private void DealDamage()
    {
        // currentHealth - currentDamageAmount
        currentHealth = healthBar.UpdateHealth(-currentDamageAmount);

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
        loseText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
