using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DetectionArea : MonoBehaviour
{
    public Material detectedMaterial;
    public EventsConsole console;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Person")) console.PrintToConsole("Person Detected");
        if (other.CompareTag("Threat"))
        {
            this.GetComponent<Renderer>().material = detectedMaterial;
            console.PrintToConsole("Nuclear Threat Detected");
        }
    }
}
