using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationDetection : MonoBehaviour
{

    [SerializeField]
    float _detectedRadAmt = 0f;

    public float DetectedRadAmt { get { return _detectedRadAmt; } set {  _detectedRadAmt = value; } }

    [SerializeField]
    List<RadiationEmission> currentRadiationSources = new List<RadiationEmission>();

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _detectedRadAmt = 0;

        if (currentRadiationSources.Count > 0)
        {

            foreach (RadiationEmission source in currentRadiationSources)
            {
                _detectedRadAmt = _detectedRadAmt + (source.radiationCalc((source.gameObject.transform.position - transform.position).magnitude));
            }
        }

        Debug.Log("Detected Radiation Amount: " + _detectedRadAmt);
    }


    public void addRadSource(RadiationEmission source)
    {
        currentRadiationSources.Add(source);
    }

    public void removeRadSource(RadiationEmission source)
    {
        if (currentRadiationSources.Contains(source))
        {
            currentRadiationSources.Remove(source);
        }
    }
}
