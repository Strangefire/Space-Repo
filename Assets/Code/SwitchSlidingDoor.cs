using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSlidingDoor : Switch
{
    public Transform doorObject;
    public Vector3 moveTo;
    public Vector3 idlePosition;

    [ContextMenu("Read Idle")]
    void ReadIdle()
    {
        idlePosition = doorObject.transform.localPosition;
    }
    [ContextMenu("Read Open")]
    void ReadOpen()
    {
        moveTo = doorObject.transform.localPosition;
    }
    private void Update()
    {
        Vector3 targetPosition = SwitchOn ? moveTo : idlePosition;
        doorObject.transform.localPosition = Vector3.Lerp(doorObject.transform.localPosition, targetPosition, Time.deltaTime * 5f);
    }
}
