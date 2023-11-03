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
        playerAttack.SpawnArrow();
    }
}
