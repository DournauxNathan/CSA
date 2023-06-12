using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event EventHandler onItemListChanged;

    private List<Item> itemList;
    public static List<Item> _itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item.stackable)
        {
            bool itemAlreadyInInventory = false;

            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.type == item.type)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        onItemListChanged?.Invoke(this, EventArgs.Empty);
        SetItemList();
    }

    public static List<Item> GetItemList()
    {
        return _itemList;
    }

    public void SetItemList()
    {
        _itemList = itemList;
    }

    public void RemoveItem(Item item)
    {
        if (item.stackable)
        {
            Item itemInInventory = null;

            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.type == item.type)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(item);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        onItemListChanged?.Invoke(this, EventArgs.Empty);
        SetItemList();
    }
}
