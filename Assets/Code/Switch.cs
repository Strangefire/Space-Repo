using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable {

    public Mesh offSwitch;
    public Mesh onSwitch;
    private bool switchOn;
    MeshFilter myFilter;
    public Switch targetSwitch;
    public bool blockUse;

    public virtual bool SwitchOn
    {
        get{return switchOn;}
        set{switchOn = value;}
    }
    public override void Awake()
    {
        base.Awake();
        myFilter = GetComponent<MeshFilter>();
    }
    public override void Use()
    {
        if (blockUse) return;
        base.Use();
        SwitchOn = !SwitchOn;
        myFilter.sharedMesh = SwitchOn ? onSwitch : offSwitch;
        if(targetSwitch)targetSwitch.SwitchOn = SwitchOn;
    }
}
