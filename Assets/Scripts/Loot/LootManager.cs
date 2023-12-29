using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class LootManager: MonoBehaviour
{
    public ArrowLootDrop arrowLootDropPrefab;

    // Player GameObject reference
    GameObject player;

    // Fixed location
    Vector3 startingArrowSpawnPoint = new Vector3(155, 2, 38);

    // Properties for spawning loot randomly around the map
    int randomRange = 50;
    float minX = 20;
    float minZ = 20;
    float maxX = 997;
    float maxZ = 997;

    // Spawn First Arrow, then periodicly spawn arrows around the map
    private void Start()
    {
        // Set Player GameObject
        player = GameObject.FindGameObjectWithTag("Player");

        // Spawn first arrow
        DropArrowLoot(startingArrowSpawnPoint, 10);
        // Spawn arrows around the map randomly
        StartCoroutine(SpawnArrowsRandomly());
    }

    // Check if Loot is dropped
    public bool IsLootDropped()
    {
        // Drop loot only 30% of the time
        return Random.Range(0, 10) < 3;
    }

    // Determine which loot to drop
    public void DropLoot(Vector3 lootSpawnPoint)
    {
        DropArrowLoot(lootSpawnPoint);
    }

    // Drop arrow loot with random number of arrows
    private void DropArrowLoot(Vector3 lootSpawnPoint) {
        // Drop a random number of arrows between 1 - 10
        int nbOfArrows = Random.Range(1, 11);
        InstantiateArrow(lootSpawnPoint, nbOfArrows);
    }

    // Drop arrow loot with a fixed number of arrows
    private void DropArrowLoot(Vector3 lootSpawnPoint, int nbOfArrows)
    {
        InstantiateArrow(lootSpawnPoint, nbOfArrows);
    }

    private void InstantiateArrow(Vector3 arrowSpawnPoint, int nbOfArrows)
    {
        // Spawn Arrow within navmesh area
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(arrowSpawnPoint, out closestHit, randomRange, NavMesh.AllAreas))
        {
            // Make arrow float in the air based on closestHit
            arrowSpawnPoint = new Vector3(closestHit.position.x, closestHit.position.y + 2, closestHit.position.z);
            // Create Arrow from Prefab
            ArrowLootDrop arrowLoot = Instantiate(arrowLootDropPrefab, arrowSpawnPoint, Quaternion.Euler(new Vector3(90, 0, 0)));
            // Set the number of arrows dropped
            arrowLoot.SetNbOfArrows(nbOfArrows);
        }
    }

    // Spawn Arrows every 45s around the player
    IEnumerator SpawnArrowsRandomly() {
        while (true)
        {
            yield return new WaitForSeconds(45f);
            // Get player position
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            float playerZ = player.transform.position.z;

            // Generate random spawn coordinates based on player's position
            float randomX = Random.Range(-randomRange + playerX, randomRange + 1 + playerX);
            float randomZ = Random.Range(-randomRange + playerZ, randomRange + 1 + playerZ);

            // Make sure coordinates are not in the water
            randomX = Mathf.Clamp(randomX, minX, maxX);
            randomZ = Mathf.Clamp(randomZ, minZ, maxZ);

            Vector3 arrowLootSpawnPoint = new Vector3(randomX, playerY, randomZ);
            DropArrowLoot(arrowLootSpawnPoint);
            Debug.Log("Spawned arrow loot");
        }
    }

    
}
