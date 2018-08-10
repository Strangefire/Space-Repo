using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLamp : Switch {

    public Light myLight;

    private void Awake()
    {
        blockUse = true;
    }
    public override bool SwitchOn
    {
        get{return base.SwitchOn;}
        set
        {
            base.SwitchOn = value;
            myLight.enabled = base.SwitchOn;
        }
    }
}
