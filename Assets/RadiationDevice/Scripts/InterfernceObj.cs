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
        transform.localScale = new Vector3(1f, 1f, 1f) * size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RadiationDetection>() != null)
        {
            other.GetComponent<RadiationDetection>().Interference = true;
            Debug.LogWarning("entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<RadiationDetection>() != null)
        {
            other.GetComponent<RadiationDetection>().Interference = false;
        }
    }
}
