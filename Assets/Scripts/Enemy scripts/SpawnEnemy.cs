using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject EnemyPrefab;
    public int enemyCount = 5;
    public TextMeshProUGUI winText;

    private int randomRange = 50;

    private float minX = 20;
    private float minZ = 20;
    private float maxX = 997;
    private float maxZ = 997;

    // Start is called before the first frame update
    void Start()
    {
        winText.gameObject.SetActive(false);
        Spawn();
    }

    // Spawn enemies around the player's location
    private void Spawn()
    {
        // Code was inspired by this tutorial: https://subscription.packtpub.com/book/game-development/9781783553655/1/ch01lvl1sec13/creating-enemies
        for (int i = 0; i < enemyCount; i++)
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            float playerZ = player.transform.position.z;

            float randomX = Random.Range(-randomRange + playerX, randomRange + 1 + playerX);
            float randomZ = Random.Range(-randomRange + playerZ, randomRange + 1 + playerZ);

            randomX = Mathf.Clamp(randomX, minX, maxX);
            randomZ = Mathf.Clamp(randomZ, minZ, maxZ);

            Instantiate(EnemyPrefab, new Vector3(randomX, playerY, randomZ), Quaternion.identity);
        }
    }

    public void UpdateEnemyCountBy(int amount)
    {
        enemyCount += amount;
        if (enemyCount <= 0)
        {
            winText.gameObject.SetActive(true);
        }
    }
}
