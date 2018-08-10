using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Strange Fire/Create Item")]
public class Item : ScriptableObject {

    public string itemName;
    public bool consumeOnUse = false;
    public int itemUses;
    public Pickup specialPrefab;
    public MeshPack lookPackage;

    private void OnEnable()
    {
        name = itemName;
    }
    public virtual bool UseItem()
    {
        itemUses = Mathf.Clamp(itemUses--,0,int.MaxValue);
        return (consumeOnUse && itemUses < 1) ? true : false;
    }
    public virtual bool SimilarItem(Item checkItem)
    {
        if (itemName == checkItem.itemName) return true;
        return false;
    }
    public virtual string Information
    {
        get
        {
            string value = itemName;
            if (consumeOnUse) value += "\nConsumed when used";
            return value;
        }
    }
}
