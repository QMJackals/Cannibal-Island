using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code was inspired by https://www.youtube.com/watch?v=yZhKUViKS_w
public class EnemyHealth : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 2;

    GameObject enemyController;

    private void Awake()
    {
        currentHealth = maxHealth;
        enemyController = GameObject.FindGameObjectWithTag("EnemyController");
    }

    public void TakeDamage(int amount) {
        Debug.Log("enemy taken damage");
        currentHealth -= amount;

        if (currentHealth <= 0) {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("enemy is dead");
        gameObject.SetActive(false);
        enemyController.GetComponent<SpawnEnemy>().UpdateEnemyCountBy(-1);
    }
}
