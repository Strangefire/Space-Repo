using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public bool canBeUsed;

    public virtual void Awake() { }
    public virtual void Start() { }
    public virtual void Use() { }
}
