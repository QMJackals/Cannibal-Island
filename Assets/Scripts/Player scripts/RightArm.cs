using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm : MonoBehaviour
{
    public GameObject player;
    PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = player.GetComponent<PlayerAttack>();
    }

    public void CallSpawnArrow()
    {
        // Spawns either a normal arrow or an explosive arrow based on the inventory current selection
        playerAttack.SpawnRangeAmmo();
    }
}
