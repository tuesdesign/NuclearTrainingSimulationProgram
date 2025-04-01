using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval;

    [SerializeField] GameObject carPrefab;

    [SerializeField]
    List<PointOfInterestBehaviour> waypoints; // A list of all possible waypoints to target

    [SerializeField]
    List<GameObject> CarList;
    // Start is called before the first frame update
    void Awake()
    {
        getAllTargets();
        InvokeRepeating("SpawnCar",0f,spawnInterval);
    }

    public void getAllTargets()
    {
        // reset waypoints
        waypoints.Clear();
        // find all objects with POI tag and add them to waypoints
        foreach (GameObject poi in GameObject.FindGameObjectsWithTag("ParkingPoint"))
        {
            waypoints.Add(poi.GetComponent<PointOfInterestBehaviour>());
        }
    }

    void SpawnCar()
    {
        //If number of cars is lower than number of parking space
        if (CarList.Count < waypoints.Count)
        {
            GameObject car = Instantiate(carPrefab, transform.position, transform.rotation);
            CarList.Add(car);

        }
    }
}
