using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code was inspired by https://www.youtube.com/watch?v=yZhKUViKS_w
public class EnemyHealth : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 2;

    GameObject enemyController;
    LootManager lootManager;

    private void Awake()
    {
        currentHealth = maxHealth;
        enemyController = GameObject.FindGameObjectWithTag("EnemyController");
        lootManager = GameObject.FindGameObjectWithTag("LootController").GetComponent<LootManager>();
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color32(224,106,95,1));

        if (currentHealth <= 0) {
            Death();
        }
    }

    void Death()
    {
        //gameObject.SetActive(false);
        enemyController.GetComponent<SpawnEnemy>().UpdateEnemyCountBy(-1);
        if (lootManager.IsLootDropped())
        {
            lootManager.DropLoot(transform.position);
        }
        Destroy(gameObject);
    }
}
