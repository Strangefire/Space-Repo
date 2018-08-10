using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Inventory : ScriptableObject {

    public List<Item> inventoryItems = new List<Item>();
    public Action<Item> OnItemReceive;
    public Action<Item> OnItemRemove;

    public void AddItem(Item item)
    {
        inventoryItems.Add(Instantiate(item));
        if (OnItemReceive != null) OnItemReceive(item);
        Destroy(item);
    }
    public void RemoveItem(Item item)
    {
        if (!inventoryItems.Contains(item)) return;
        if (OnItemRemove != null) OnItemRemove(item);
        inventoryItems.Remove(item);
    }
}
