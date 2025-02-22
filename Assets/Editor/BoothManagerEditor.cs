using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoothManager))]
public class BoothManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BoothManager boothManager = (BoothManager)target;

        if (GUILayout.Button("Add Queue"))
        {
            boothManager.AddBoothLineTarget();
        }

        if (GUILayout.Button("Update Queue"))
        {
            boothManager.UpdateBoothLine();
        }
    }
}
