using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour {

    Pickup myPickup;
    public MeshFilter targetFilter;
    public List<Mesh> meshStages;
    int lastUse = -1;

    private void Awake()
    {
        myPickup = GetComponent<Pickup>();
        if (myPickup.pickupItem.lookPackage == null) return;
        targetFilter.mesh = myPickup.pickupItem.lookPackage.theMesh;
        targetFilter.GetComponent<Renderer>().materials = myPickup.pickupItem.lookPackage.materials.ToArray();
    }
    private void Update()
    {
        if (myPickup.pickupItem != null && myPickup.pickupItem is Food)
        {
            Food foodItem = myPickup.pickupItem as Food;
            int currentStage = foodItem.itemUses;
            if(currentStage != lastUse)
            {
                lastUse = currentStage;
                if (meshStages.Count < lastUse) return;
                targetFilter.sharedMesh = meshStages[lastUse];
            }
        }
    }
}
