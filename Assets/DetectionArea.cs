using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DetectionArea : MonoBehaviour
{
    public Material detectedMaterial;
    public EventsConsole console;

    public string DetectorName; // name of the detector to print

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Person")) console.PrintToConsole(DetectorName +": Person Detected");
        if (other.CompareTag("Threat"))
        {
            this.GetComponent<Renderer>().material = detectedMaterial;
            console.PrintToConsole("NUCLEAR THREAT DETECTED BY "+DetectorName);
        }
    }
}
