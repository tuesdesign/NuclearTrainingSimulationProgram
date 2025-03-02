using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDetector : MonoBehaviour
{

    bool triggered = false;

    public bool Triggered { get  { return triggered; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //checks if a radioactive object enters or leaves the detector
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Radioactive"))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Radioactive"))
        {
            triggered = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Radioactive"))
        {
            triggered = true;
        }
    }

}
