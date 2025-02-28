using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private LevelEditorManager reference;

    public void Save()
    {

        reference.bakeNavMesh();
        Debug.Log("Baked NavMesh (supposedly)");
    }
}
