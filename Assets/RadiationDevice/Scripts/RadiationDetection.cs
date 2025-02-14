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

    [SerializeField]
    float _threshold = 5f;

    [SerializeField]
    SO_BackgroundRadiation _BRValues;

    private float _currBGRad;

    private bool _thresholdReached = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Resets detected radiation amount at start of every frame so it doesn't scale improperly
        _detectedRadAmt = 0;

        //checks if there are any radiation sources that the detector is inside of
        if (currentRadiationSources.Count > 0)
        {
            //for every radiation source it calculates and adds the radiation amount detected from the source to the current detected amount
            foreach (RadiationEmission source in currentRadiationSources)
            {
                _detectedRadAmt = _detectedRadAmt + (source.radiationCalc((source.gameObject.transform.position - transform.position).magnitude));
            }
        }

        //retrieves a random value in between the min and max values from the scriptable object to act as a background radiation parameter
        //then adds that value to the detected radiation amount
        _currBGRad = Random.Range(_BRValues.min_BG_Radiation, _BRValues.max_BG_Radiation);
        _detectedRadAmt += _currBGRad;

        //checks if the detected radiation amount has surpassed the set threshold amount and sets the bool to true or false respectively
        if(_detectedRadAmt > _threshold) _thresholdReached = true;
        else _thresholdReached = false;

        //prints out debug values
        Debug.Log("Detected Radiation Amount: " + _detectedRadAmt + "   Threshold Reached? " + _thresholdReached);
    }


    //method called by radiation emitter to add itself to the list of sources in the detector
    public void addRadSource(RadiationEmission source)
    {
        currentRadiationSources.Add(source);
    }

    //method called by radiation emitter to remove itself from the list of sources in the detector
    public void removeRadSource(RadiationEmission source)
    {
        if (currentRadiationSources.Contains(source))
        {
            currentRadiationSources.Remove(source);
        }
    }
}
