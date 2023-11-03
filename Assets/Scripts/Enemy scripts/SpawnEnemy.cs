using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject EnemyPrefab;
    public int maxEnemyCount = 5;
    public int numOfWaves = 2;
    public TextMeshProUGUI winText;
    public InGameTimer gameTimer;

    private int randomRange = 50;

    private float minX = 20;
    private float minZ = 20;
    private float maxX = 997;
    private float maxZ = 997;

    int enemyCount = 0;
    int waves = 0;

    // Start is called before the first frame update
    void Start()
    {
        winText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameTimer.timeCycle == TimeCycle.NIGHT && waves < numOfWaves && (waves == 0 || enemyCount <= 3))
        {
            Spawn();
        }
    }

    // Spawn enemies around the player's location
    private void Spawn()
    {
        // Code was inspired by this tutorial: https://subscription.packtpub.com/book/game-development/9781783553655/1/ch01lvl1sec13/creating-enemies
        for (int i = 0; i < maxEnemyCount; i++)
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            float playerZ = player.transform.position.z;

            Debug.Log(player.transform.position);

            float randomX = Random.Range(-randomRange + playerX, randomRange + 1 + playerX);
            float randomZ = Random.Range(-randomRange + playerZ, randomRange + 1 + playerZ);

            randomX = Mathf.Clamp(randomX, minX, maxX);
            randomZ = Mathf.Clamp(randomZ, minZ, maxZ);

            Instantiate(EnemyPrefab, new Vector3(randomX, playerY, randomZ), Quaternion.identity);
        }
        enemyCount += maxEnemyCount;
        waves++;
    }

    public void UpdateEnemyCountBy(int amount)
    {
        // This function is used to decrement the enemy count when the player kills enemies
        enemyCount += amount;
        if (enemyCount <= 0 && waves >= numOfWaves)
        {
            winText.gameObject.SetActive(true);
        }
    }
}
