using UnityEditor;
using UnityEngine;

public class EditorUtilities : Editor {

    public static bool[] inventoryToggleBool = new bool[0];

    public static void DisplayInventory(Inventory theInventory)
    {
        GUILayout.BeginVertical("GroupBox");
        GUILayout.Label("Items in inventory");
        if (theInventory.inventoryItems.Count < 1) GUILayout.Label("Inventory Empty");
        if (inventoryToggleBool.Length < theInventory.inventoryItems.Count) inventoryToggleBool = new bool[theInventory.inventoryItems.Count];
        for (int i = 0; i < theInventory.inventoryItems.Count; i++)
        {
            inventoryToggleBool[i] = EditorGUILayout.Foldout(inventoryToggleBool[i],string.Format("[{0}]{1}", i, theInventory.inventoryItems[i].itemName));
            if(inventoryToggleBool[i])
            {
                GUILayout.BeginVertical("GroupBox");
                Item itemReference = theInventory.inventoryItems[i];
                if(itemReference is Food)
                {
                    if ((itemReference as Food).changeEnergy > 0f) GUILayout.Label(string.Format("{0} Energy", (itemReference as Food).changeEnergy));
                    if ((itemReference as Food).changeThirst > 0f)GUILayout.Label(string.Format("{0} Thirst", (itemReference as Food).changeThirst));
                    if((itemReference as Food).changeHunger > 0f)GUILayout.Label(string.Format("{0} Hunger", (itemReference as Food).changeHunger));
                    if (Application.isPlaying)
                    {
                        if (Player.Global != null && Player.Global.playerInventory != null && Player.Global.playerInventory.inventoryItems.Contains(itemReference))
                        {
                            if (GUILayout.Button("Use Item"))
                            {
                                Player.Global.UseItem(itemReference);
                            }
                        }
                    }
                }
                GUILayout.EndVertical();
            }
        }
        GUILayout.EndVertical();
    }
}
