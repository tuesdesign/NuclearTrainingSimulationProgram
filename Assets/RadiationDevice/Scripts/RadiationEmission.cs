using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RadiationEmission : MonoBehaviour
{

    float directions = 6;

    Vector3 direction;
    LayerMask detectorMask;


    [SerializeField]
    float size;

    [SerializeField]
    float strength;




    // Start is called before the first frame update
    void Start()
    {
        detectorMask = LayerMask.GetMask("Detector");
    }

    private void Awake()
    {
        transform.localScale = new Vector3(1f,1f,1f)*size;
    }

    // Update is called once per frame
    void Update()
    {


        for(int i = 0; i < directions; i++)
        {
            //CHANGE THIS TO USE DEGREES SO IT'S WAY CLEANER AND NICER
            //THIS IS JUST A QUICK MOCK UP BUT IT'S NOT MODULAR ENOUGH!!!!!!
            switch (i)
            {
                case 0:
                    direction = transform.TransformDirection(-Vector3.up);
                    break;
                case 1:
                    direction = transform.TransformDirection(Vector3.up);
                    break;
                case 2:
                    direction = transform.TransformDirection(Vector3.forward);
                    break;
                case 3:
                    direction = transform.TransformDirection(-Vector3.forward);
                    break;
                case 4:
                    direction = transform.TransformDirection(-Vector3.right);
                    break;
                case 5:
                    direction = transform.TransformDirection(Vector3.right);
                    break;
            }

            if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, 20f, detectorMask))
            {
                if (hitInfo.transform.CompareTag("Detector"))
                {
                    Debug.Log("hit detector");
                    Debug.DrawLine(transform.position, hitInfo.point, Color.green);
                }

            }

            
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<RadiationDetection>() != null)
        {
            other.GetComponent<RadiationDetection>().addRadSource(this);
        }
    }
    

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
        //strength of radiation multiplied by 
        float val = strength * (((size / 2) - distance));

        if(val < 0)
        {
            val = 0;
        }

        return val;
        
    }
}
