using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationDeviceRandomizer : MonoBehaviour
{
    [SerializeField] List<GameObject> personArray = new List<GameObject>();
    [SerializeField] int maxIndexOfPersonWithDevice;
    [SerializeField] int indexOfPersonWithDevice;

    [SerializeField] GameObject nuclearDevicePrefab;
    bool deviceGiven;

    public GameObject nuclearDevice;
    // Start is called before the first frame update
    void Start()
    {
        //Randomize the Index who will have the nuclear device
        indexOfPersonWithDevice = Random.Range(1, maxIndexOfPersonWithDevice + 1);
    }

    // Update is called once per frame
    void Update()
    {
        //If the device doesn't exist
        if (!deviceGiven)
        {
            CountPeople();
            AddDevice();
        }

    }

    void CountPeople()
    {
        
        
        //Put every person in a list
        personArray.Clear();
        GameObject[] persons = GameObject.FindGameObjectsWithTag("Person");

        foreach (GameObject person in persons)
        {
            personArray.Add(person);
        }
        
    }

    void AddDevice()
    {
        //If the list of persons is more or at the index of the person that would have the device
        if(personArray.Count >= indexOfPersonWithDevice)
        {
            GameObject parent = personArray[indexOfPersonWithDevice].gameObject;

            //Give the person with the correct index the nuclear device
            nuclearDevice = Instantiate(nuclearDevicePrefab, parent.transform.position, parent.transform.rotation, parent.transform);
            deviceGiven = true;
        }
    }

    public void ScaleNuclearDevice(float scale)
    {
        if(nuclearDevice != null)
        {
            nuclearDevice.GetComponent<ObjectScaleProperty>().ScaleObject(scale);
        }
    }

}
