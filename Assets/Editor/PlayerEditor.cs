using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor {

    bool[] enableToggle = new bool[1];
    Player myPlayer;

    private void OnEnable()
    {
        myPlayer = target as Player;
    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();
        EditorGUI.ProgressBar(new Rect(EditorGUILayout.GetControlRect().position.x, EditorGUILayout.GetControlRect().position.y, EditorGUIUtility.currentViewWidth - 20f, 20f), myPlayer.CurrentHealth / myPlayer.MaxHealth, string.Format("HP: {0}/{1}", myPlayer.CurrentHealth.ToString("F2"), myPlayer.MaxHealth));
        if (Application.isPlaying && GUILayout.Button("Full Health")) myPlayer.FullHealth();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        EditorGUI.ProgressBar(new Rect(EditorGUILayout.GetControlRect().position.x, EditorGUILayout.GetControlRect().position.y, EditorGUIUtility.currentViewWidth - 20f, 20f), myPlayer.CurrentHunger / myPlayer.MaxHunger, string.Format("Hunger: {0}/{1}", myPlayer.CurrentHunger.ToString("F2"), myPlayer.MaxHunger));
        if (Application.isPlaying && GUILayout.Button("No Hunger")) myPlayer.FullHunger();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        EditorGUI.ProgressBar(new Rect(EditorGUILayout.GetControlRect().position.x, EditorGUILayout.GetControlRect().position.y, EditorGUIUtility.currentViewWidth - 20f, 20f), myPlayer.CurrentThirst / myPlayer.MaxThirst, string.Format("Thirst: {0}/{1}", myPlayer.CurrentThirst.ToString("F2"), myPlayer.MaxThirst));
        if (Application.isPlaying && GUILayout.Button("No Thirst")) myPlayer.FullThirst();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        EditorGUI.ProgressBar(new Rect(EditorGUILayout.GetControlRect().position.x, EditorGUILayout.GetControlRect().position.y, EditorGUIUtility.currentViewWidth - 20f, 20f), myPlayer.CurrentEnergy / myPlayer.MaxEnergy, string.Format("Energy: {0}/{1}", myPlayer.CurrentEnergy.ToString("F2"), myPlayer.MaxEnergy));
        if (Application.isPlaying && GUILayout.Button("Full Energy")) myPlayer.FullEnergy();
        GUILayout.EndVertical();
        if (Application.isPlaying)
        {
            if (myPlayer.playerInventory != null)
            {
                EditorUtilities.DisplayInventory(myPlayer.playerInventory);
            }
        }
        else GUILayout.Space(10);
        if (GUILayout.Button(enableToggle[0] ? "Close Variables" : "Open Variables")) enableToggle[0] = !enableToggle[0];
        if(enableToggle[0])
        {
            GUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Health",GUILayout.Width(80));
            myPlayer.CurrentHealth = EditorGUILayout.FloatField(myPlayer.CurrentHealth);
            myPlayer.MaxHealth = EditorGUILayout.FloatField(myPlayer.MaxHealth);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Hunger", GUILayout.Width(80));
            myPlayer.CurrentHunger = EditorGUILayout.FloatField(myPlayer.CurrentHunger);
            myPlayer.MaxHunger = EditorGUILayout.FloatField(myPlayer.MaxHunger);
            myPlayer.increaseHungerPerSecond = EditorGUILayout.FloatField("Increase Hunger p/s:", myPlayer.increaseHungerPerSecond);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Thirst", GUILayout.Width(80));
            myPlayer.CurrentThirst = EditorGUILayout.FloatField(myPlayer.CurrentThirst);
            myPlayer.MaxThirst = EditorGUILayout.FloatField(myPlayer.MaxThirst);
            myPlayer.increaseThirstPerSecond = EditorGUILayout.FloatField("Increase Thirst p/s:", myPlayer.increaseThirstPerSecond);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Energy", GUILayout.Width(80));
            myPlayer.CurrentEnergy = EditorGUILayout.FloatField(myPlayer.CurrentEnergy);
            myPlayer.MaxEnergy = EditorGUILayout.FloatField(myPlayer.MaxEnergy);
            myPlayer.drainEnergyPerSecond = EditorGUILayout.FloatField("Drain Energy p/s:", myPlayer.drainEnergyPerSecond);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
            myPlayer.movementSpeed = EditorGUILayout.FloatField("Movement Speed:", myPlayer.movementSpeed);
            myPlayer.lookRange = EditorGUILayout.FloatField("Look Range", myPlayer.lookRange);
            myPlayer.jumpForce = EditorGUILayout.FloatField("Jump Force", myPlayer.jumpForce);
            GUILayout.EndVertical();
        }
        Repaint();
    }
}
