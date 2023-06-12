using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, ICollectable
{
    public Item item;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.image;
    }
    
    public void Collect(Inventory _inventory)
    {
        _inventory.AddItem(item);
        Destroy(this.gameObject);
    }

}
