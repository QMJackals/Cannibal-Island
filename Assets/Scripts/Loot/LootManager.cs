using UnityEngine;
using System.Collections;

public class LootManager: MonoBehaviour
{
    public ArrowLootDrop arrowLootDropPrefab;

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

    private void DropArrowLoot(Vector3 lootSpawnPoint) {
        // Drop a random number of arrows between 1 - 10
        int nbOfArrows = Random.Range(1, 11);
        ArrowLootDrop currLoot = Instantiate(arrowLootDropPrefab, lootSpawnPoint, Quaternion.Euler(new Vector3(90, 0, 0)));
        // Set the number of arrows dropped
        currLoot.nbOfArrows = nbOfArrows;
    }
}
