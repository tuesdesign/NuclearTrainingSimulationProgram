using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationManager : MonoBehaviour
{
    //lists fill on awake

    [SerializeField] List<RadiationDetection> detectors = new List<RadiationDetection>();
    [SerializeField] List<RadiationEmission> emitters = new List<RadiationEmission>();
    [SerializeField] List<GateDetector> walkthroughDetectors = new List<GateDetector>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
        foreach(RadiationEmission emitter in GameObject.FindObjectsOfType<RadiationEmission>())
        {
            emitters.Add(emitter);
        }

        foreach(RadiationDetection detector in GameObject.FindObjectsOfType<RadiationDetection>())
        {
            detectors.Add(detector);
        }

        foreach(GateDetector gateDetector in GameObject.FindObjectsOfType<GateDetector>())
        {
            walkthroughDetectors.Add(gateDetector);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
