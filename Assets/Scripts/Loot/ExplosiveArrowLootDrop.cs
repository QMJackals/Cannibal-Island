using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplosiveArrowLootDrop : MonoBehaviour
{
    public TextMeshPro arrowLabel;

    int nbOfArrows = 1;

    private void Start()
    {
        StartCoroutine(Despawn());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Inventory>().AddItem(InventoryItemType.EXPLOSIVE_ARROW, nbOfArrows);
            Destroy(gameObject);
        }
    }

    private IEnumerator Despawn()
    {
        // Despawn after 30 seconds
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }

    public void SetNbOfArrows(int nb)
    {
        nbOfArrows = nb;
        arrowLabel.text = string.Format("{0} Explosive Arrows", nbOfArrows);
    }
}
