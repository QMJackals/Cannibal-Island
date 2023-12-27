using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLootDrop: MonoBehaviour
{
    public int nbOfArrows = 1;

    private void Start()
    {
        StartCoroutine(Despawn());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Inventory>().AddItem(InventoryItemType.ARROW, nbOfArrows);
            Destroy(gameObject);
        }
    }

    private IEnumerator Despawn() {
        // Despawn after 30 seconds
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }
}
