using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour {

    public Item itemToSpawn;

    private void Start()
    {
        GameControl.SpawnPickup(Instantiate(itemToSpawn),transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one/2f);
    }
}
