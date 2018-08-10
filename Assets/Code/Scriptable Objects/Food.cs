using UnityEngine;

[CreateAssetMenu(fileName = "New Food",menuName = "Strange Fire/Create Food")]
public class Food : Item {

    public float changeHunger = 0f;
    public float changeThirst = 0f;
    public float changeEnergy = 0f;

    public override bool UseItem()
    {
        Player.Global.CurrentHunger += changeHunger;
        Player.Global.CurrentThirst += changeThirst;
        Player.Global.CurrentEnergy += changeEnergy;
        return base.UseItem();
    }
    public override string Information
    {
        get
        {
            string baseInfo = base.Information;
            if (Mathf.Abs(changeHunger) > 0f) baseInfo += string.Format("\n{0}{1} Hunger", (changeHunger > 0f) ? "-" : "+", Mathf.Abs(changeHunger));
            if (Mathf.Abs(changeThirst) > 0f) baseInfo += string.Format("\n{0}{1} Thirst", (changeThirst > 0f) ? "-" : "+", Mathf.Abs(changeThirst));
            if (Mathf.Abs(changeEnergy) > 0f) baseInfo += string.Format("\n{0}{1} Energy", (changeEnergy > 0f) ? "-" : "+", Mathf.Abs(changeEnergy));
            return baseInfo;
        }
    }
}
