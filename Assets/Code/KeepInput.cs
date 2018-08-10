using System.Collections.Generic;
using UnityEngine;

public class KeepInput : MonoBehaviour {

    public static Dictionary<string, KeyCode> inputKeys = new Dictionary<string, KeyCode>();

    public void DefaultKeys()
    {
        inputKeys.Add("Forward", KeyCode.W);
        inputKeys.Add("Backwards", KeyCode.S);
        inputKeys.Add("Strafe Left", KeyCode.A);
        inputKeys.Add("Strafe Right", KeyCode.D);
        inputKeys.Add("Jump", KeyCode.Space);
        inputKeys.Add("Pick Item", KeyCode.Q);
        inputKeys.Add("Interact", KeyCode.E);
        inputKeys.Add("Throw", KeyCode.Mouse1);
        inputKeys.Add("Choose Item", KeyCode.Tab);
        inputKeys.Add("Use Item", KeyCode.F);
        inputKeys.Add("Left Use", KeyCode.Mouse0);
    }
    public static string ReturnKeyName(string key)
    {
        if (!inputKeys.ContainsKey(key)) return "[N/A]";
        return "["+inputKeys[key]+"]";
    }
    private void Start()
    {
        DefaultKeys();
    }
    public static bool KeyDown(string keyName)
    {
        if (!inputKeys.ContainsKey(keyName)) return false;
        return Input.GetKeyDown(inputKeys[keyName]);
    }
    public static bool KeyUp(string keyName)
    {
        if (!inputKeys.ContainsKey(keyName)) return false;
        return Input.GetKeyUp(inputKeys[keyName]);
    }
    public static bool Key(string keyName)
    {
        if (!inputKeys.ContainsKey(keyName)) return false;
        return Input.GetKey(inputKeys[keyName]);
    }
}
