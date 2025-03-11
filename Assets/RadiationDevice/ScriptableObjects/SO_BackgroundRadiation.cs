using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BG_Radiation_Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SO_BackgroundRadiation : ScriptableObject
{
    public float min_BG_Radiation = 0;

    public float max_BG_Radiation = 10;
}
