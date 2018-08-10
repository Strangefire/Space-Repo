using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherBody = other.GetComponent<Rigidbody>();
        if (!otherBody) return;
        if (!otherBody.isKinematic) otherBody.useGravity = false;
    }
    private void OnTriggerExit(Collider other)
    {
        Rigidbody otherBody = other.GetComponent<Rigidbody>();
        if (!otherBody) return;
        if (!otherBody.isKinematic) otherBody.useGravity = true;
    }
}
