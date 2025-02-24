using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject ConfigureAndEvents;
    [SerializeField]
    GameObject LevelEditor;

    public void ToggleUI()
    {
        ConfigureAndEvents.SetActive(!ConfigureAndEvents.activeInHierarchy);
        LevelEditor.SetActive(!LevelEditor.activeInHierarchy);

    }
}
