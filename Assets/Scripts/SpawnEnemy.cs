using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int enemyCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        // Code was inspired by this tutorial: https://subscription.packtpub.com/book/game-development/9781783553655/1/ch01lvl1sec13/creating-enemies
        for (int i = 0; i < enemyCount; i++)
        {
            float randomX = Random.Range(-3, 4);
            float randomZ = Random.Range(-3, 4);

            Instantiate(EnemyPrefab, new Vector3(randomX, 2, randomZ), Quaternion.identity);
        }
    }
}
