using UnityEngine;

public class GameControl : MonoBehaviour {

    public static GameControl Global;
    public Pickup pickupPrefab;

    private void Awake()
    {
        Global = this;
    }
    private void OnDestroy()
    {
        Global = null;
    }
    public void DamageObject(IDamageable targetObject)
    {
        targetObject.Damage(new DamagePack(){ damageAmount = 10f });
    }
    public static Pickup SpawnPickup(Item item,Vector3 position,Quaternion rotation)
    {
        Pickup thePickup = null;
        if (item.specialPrefab == null) thePickup = Instantiate(Global.pickupPrefab);
        else thePickup = Instantiate(item.specialPrefab);
        thePickup.takeToInventory = true;
        thePickup.pickupItem = Instantiate(item);
        thePickup.transform.position = position;
        thePickup.transform.rotation = Quaternion.identity;
        Destroy(item);
        return thePickup;
    }
}
