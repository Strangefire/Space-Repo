using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool",menuName = "Strange Fire/Create Tool")]
public class Tool : Item {

    public enum ToolItem
    {
        None,
        Flashlight
    }
    public ToolItem currentTool = ToolItem.None;
    public bool usingTool;

    public override bool UseItem()
    {
        if (currentTool == ToolItem.Flashlight) usingTool = !usingTool;
        return base.UseItem();
    }
    public override string Information
    {
        get
        {
            string baseInfo = base.Information;
            if(currentTool == ToolItem.Flashlight)
            {
                baseInfo += string.Format("\n{0}", usingTool ? "Turned On" : "Turned Off");
            }
            return baseInfo;
        }
    }
}
