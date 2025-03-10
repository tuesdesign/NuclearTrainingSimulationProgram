using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationManager : MonoBehaviour
{
    //lists fill on awake

    [SerializeField] public List<RadiationDetection> stationaryDetectors = new List<RadiationDetection>();
    [SerializeField] public List<RadiationDetection> movingDetectors = new List<RadiationDetection>();
    [SerializeField] public List<RadiationEmission> emitters = new List<RadiationEmission>();

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
            if(detector.detectorType == DetectorType.Stationary)
            {
                stationaryDetectors.Add(detector);
            }
            else if(detector.detectorType == DetectorType.Moving)
            {
                movingDetectors.Add(detector);
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
