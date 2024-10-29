using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DetectionArea : MonoBehaviour
{
    public EventsConsole console;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Person")) console.PrintToConsole("Person Detected");
        if (other.CompareTag("Threat")) console.PrintToConsole("Nuclear Threat Detected");
    }
}
