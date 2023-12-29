using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject EnemyPrefab;
    public TextMeshProUGUI winText;
    public InGameTimer gameTimer;

    // Properties for spawning enemies randomly around the map
    private int randomRange = 50;
    private float minX = 20;
    private float minZ = 20;
    private float maxX = 997;
    private float maxZ = 997;

    // Enemy wave properties
    int enemyCount = 0;
    int waves = 0;
    int maxEnemyCount = 20;
    int numOfWaves = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Hide win text
        winText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Only spawn enemy's at night 
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
            // Get player position
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            float playerZ = player.transform.position.z;

            // Generate random coordinates for enemies based on player position
            float randomX = Random.Range(-randomRange + playerX, randomRange + 1 + playerX);
            float randomZ = Random.Range(-randomRange + playerZ, randomRange + 1 + playerZ);

            // Make sure enemy spawn location is on the map
            randomX = Mathf.Clamp(randomX, minX, maxX);
            randomZ = Mathf.Clamp(randomZ, minZ, maxZ);

            // Spawn enemy within navmesh area based on solutions from https://forum.unity.com/threads/failed-to-create-agent-because-it-is-not-close-enough-to-the-navmesh.125593/
            Vector3 enemySpawnLocation = new Vector3(randomX, playerY - 1, randomZ);
            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(enemySpawnLocation, out closestHit, randomRange, NavMesh.AllAreas))
            {
                Instantiate(EnemyPrefab, closestHit.position, Quaternion.identity);
                enemyCount++;
            }
        }
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
