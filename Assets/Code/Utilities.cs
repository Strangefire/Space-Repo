using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {

    public static char RandomCharacter
    {
        get { return (char)('a' + Random.Range(0, 26)); }
    }
    public static Inventory PlayerInventory
    {
        get { return Player.Global.playerInventory; }
    }
    public static List<Item> PlayerItems
    {
        get { return Player.Global.playerInventory.inventoryItems; }
    }
}
[System.Serializable]
public class MeshPack
{
    public Mesh theMesh;
    public List<Material> materials = new List<Material>();
}
