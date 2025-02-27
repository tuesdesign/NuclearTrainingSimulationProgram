using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RadiationEmission : MonoBehaviour
{



    [SerializeField]
    float size;

    [SerializeField]
    float strength;




    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        transform.localScale = new Vector3(1f,1f,1f)*size;
    }

    // Update is called once per frame
    void Update()
    {



    }


    //if detector enters the trigger, add emitter to source list in detector
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<RadiationDetection>() != null)
        {
            other.GetComponent<RadiationDetection>().addRadSource(this);
        }
    }
    

    //if detector leaves the trigger, remove emitter from the source list in detector
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<RadiationDetection>() != null)
        {
            other.GetComponent<RadiationDetection>().removeRadSource(this);
        }

    }




    //Function for calculating radiation amount
    public float radiationCalc(float distance)
    {
        //strength of radiation multiplied by radius subtract distance of detector to emitter
        float val = strength * (((size / 2) - distance));

        if(val < 0)
        {
            val = 0;
        }

        return val;
        
    }
}
