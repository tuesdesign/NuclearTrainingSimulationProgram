using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(PersonNavigator))]
public class PersonNavigatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PersonNavigator personNavigator = (PersonNavigator)target;

        if (GUILayout.Button("Debug Change Target"))
        {
            personNavigator.SetNewTarget();
        }

        
    }
}
