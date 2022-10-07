using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public ItemSO item;
    public GameObject inventoryGameObject;

    public InventoryItem(ItemSO item, GameObject inventoryGameObject)
    {
        this.item = item;
        this.inventoryGameObject = inventoryGameObject;
    }
}
