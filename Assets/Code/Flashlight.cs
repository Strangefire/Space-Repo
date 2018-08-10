using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    Pickup myPickup;
    public Light flashlightLight;

    private void Awake()
    {
        myPickup = GetComponent<Pickup>();
    }
    private void Update()
    {
        if (myPickup.pickupItem != null && myPickup.pickupItem is Tool)
        {
            if ((myPickup.pickupItem as Tool).currentTool == Tool.ToolItem.Flashlight)
            {
                flashlightLight.enabled=((myPickup.pickupItem as Tool).usingTool);
            }
        }
    }
}
