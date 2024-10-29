using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaleProperty : MonoBehaviour
{
    public void ScaleObject(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
