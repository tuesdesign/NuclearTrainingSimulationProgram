using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationDetection : MonoBehaviour
{

    [Header("Type of Detector:")]

    [SerializeField]
    DetectorType _dtype;

    public DetectorType detectorType { get { return _dtype; } }


    [Header("Detected Amount:")]
    [SerializeField]
    float _detectedRadAmt = 0f;

    

    public float DetectedRadAmt { get { return _detectedRadAmt; } set {  _detectedRadAmt = value; } }
    public float Threshold { get { return _threshold; } set {  _threshold = value; } }

    [SerializeField]
    List<RadiationEmission> currentRadiationSources = new List<RadiationEmission>();

    [SerializeField]
    float _threshold = 5f;

    [SerializeField]
    SO_BackgroundRadiation _BRValues;

    [SerializeField] private ClusterManager manager;

    private float _currBGRad;

    private bool _thresholdReached = false;

    private bool _interference = false;

    public bool Interference { get { return _interference; } set { _interference = value; } }

    private bool _malfunction = false;

    public bool Malfunction { get { return _malfunction; } set { _malfunction = value; } }



    private InterfernceObj _interferenceObj;
    public InterfernceObj InterferenceObj { private get { return _interferenceObj; } set { _interferenceObj = value; } }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Resets detected radiation amount at start of every frame so it doesn't scale improperly
        _detectedRadAmt = 0;

        if (!_malfunction)
        {
            
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
            if (_detectedRadAmt > _threshold)_thresholdReached = true;
            else _thresholdReached = false;

            if (!_interference)
            {
                //if there's no interference, set the interference reference to null
                if (_interferenceObj != null)
                {
                    _interferenceObj = null;
                }

            }
            else
            {
                //if there is interference, modify the detected radiation amount to subtract a random amount depending on how far in the interference range it is
                _detectedRadAmt = _detectedRadAmt - Random.Range(0, _detectedRadAmt * _interferenceObj.GetDistance(transform.position));
            }
        }
        else
        {
            _detectedRadAmt = Random.Range(0, 200);
        }


        

        //prints out debug values
        Debug.Log("Detected Radiation Amount: " + _detectedRadAmt + "   Threshold Reached? " + _thresholdReached + "    Interference? " + _interference);
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
    public bool getThreSholdReached()
    {
        return _thresholdReached;
    }

}


public enum DetectorType
{
    Moving,
    Stationary,
}
