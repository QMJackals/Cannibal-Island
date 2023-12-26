using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLootDrop: MonoBehaviour
{
    public int nbOfArrows = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player picked up " + nbOfArrows + " arrows");
            Destroy(gameObject);
        }
    }
}
