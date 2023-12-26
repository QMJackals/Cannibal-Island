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
        currentHealth -= amount;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);

        if (currentHealth <= 0) {
            Death();
        }
    }

    void Death()
    {
        gameObject.SetActive(false);
        enemyController.GetComponent<SpawnEnemy>().UpdateEnemyCountBy(-1);
    }
}
