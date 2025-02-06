using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationEmission : MonoBehaviour
{

    float directions = 6;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
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

            if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, 20f))
            {
                if (hitInfo.transform.CompareTag("Detector"))
                {
                    Debug.Log("hit detector");
                    Debug.DrawLine(transform.position, hitInfo.point, Color.green);
                }

            }

            
        }

    }
}
