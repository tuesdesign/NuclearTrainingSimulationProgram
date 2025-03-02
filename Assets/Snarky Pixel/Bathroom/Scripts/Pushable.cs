using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : InteractiveObject
{
    public float pushForce = 400.0f;

    // This function is called whenever the object is interacted with
    public override void Action()
    {
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * pushForce);
    }
}

