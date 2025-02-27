using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfernceObj : MonoBehaviour
{

    [SerializeField]
    float size;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //set the scale to selected scale
        transform.localScale = new Vector3(1f, 1f, 1f) * size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when a radiation detector enters the space, set internal interference variable to true and set interferenceobj to itself
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RadiationDetection>() != null)
        {
            other.GetComponent<RadiationDetection>().Interference = true;
            other.GetComponent<RadiationDetection>().InterferenceObj = this;
            Debug.LogWarning("entered");
        }
    }

    //when a detector leaves teh interference range, set the internal interference variable to false
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<RadiationDetection>() != null)
        {
            other.GetComponent<RadiationDetection>().Interference = false;
        }
    }


    //method for getting distance of object to interference center
    public float GetDistance(Vector3 position)
    {
        float val = ((transform.position - position).magnitude) / (size/2);

        val = Mathf.Clamp(val, 0, 1);

        return val;
        
    }
}
