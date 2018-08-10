using UnityEngine;

public class SceneControl : MonoBehaviour {

    public GameObject[] coreObjects;

    private void Awake()
    {
        for (int i = 0; i < coreObjects.Length; i++)
        {
            Instantiate(coreObjects[i]);
        }
    }
}
