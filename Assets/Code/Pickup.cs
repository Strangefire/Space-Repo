using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickup : Interactable {

    public Rigidbody myBody;
    public Item pickupItem;
    public bool takeToInventory;
    Collider myCollider;

    public void TogglePickup(bool value)
    {
        if (value)
        {
            gameObject.layer = 0;
            myBody.constraints = RigidbodyConstraints.None;
        }
        else
        {
            gameObject.layer = 11;
            myBody.constraints = RigidbodyConstraints.FreezeAll;
        }
        myBody.isKinematic = !value;
        
        myCollider.enabled = value;
    }
    public override void Awake()
    {
        base.Awake();
        myBody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
    }
    public override void Start()
    {
        base.Start();
        pickupItem = Instantiate(pickupItem);
        gameObject.name = "Pickup_" + pickupItem.itemName;
    }
}
