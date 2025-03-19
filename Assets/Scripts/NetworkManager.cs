using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviour
{
    [Serializable]

    //custom class used to associate the detectors to their UI elements (needs to be hardcoded in editor)
    public class UIAssociation
    {
        public string name;
        public RadiationDetection RadiationDetector;
        public GeigerCounter GeigerCounter;
        public bool packetLoss;
    }

    public List<UIAssociation> associations;

    [SerializeField] float latencyInSecs = 1;

    [SerializeField] int percentageOfPacketLoss = 33;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method called by sensors, starts the delay corountine
    public void ReceiveDataFromSensor(float data, RadiationDetection detector)
    {
        StartCoroutine(delay(latencyInSecs, detector, data));
    }


    
    IEnumerator delay(float delay, RadiationDetection detector, float data)
    {
        yield return new WaitForSeconds(delay);
        SendToUI(detector, data);
    }


    //once called, checks the associations list for the current detector, then sends the associated UI element the data being fed while taking into account packet loss
    void SendToUI(RadiationDetection detector, float data)
    {
        foreach (var association in associations)
        {
            if (association.RadiationDetector == detector)
            {
                if (association.packetLoss)
                {
                    int randVar = Random.Range(0, 100);

                    if(randVar >= percentageOfPacketLoss)
                    {
                        association.GeigerCounter.receiveData(data);
                    }
                    else
                    {
                        //association.GeigerCounter.receiveData(0);
                    }
                    
                }
                else
                {
                    association.GeigerCounter.receiveData(data);
                }

            }
        }
    }
}
