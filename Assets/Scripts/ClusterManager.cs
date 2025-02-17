using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ClusterManager : MonoBehaviour
{
    
    [SerializeField] private List<Transform> civCluster1Waypoints;
    [SerializeField] private List<Transform> firstRespondersCluster1Waypoints;
    [SerializeField] private List<Transform> threatCluster1Waypoints;
    [SerializeField] private List<Transform> eventEmployeeCluster1Waypoints;

    private void Awake()
    {
        getAllTargets(civCluster1Waypoints, "POI");
        getAllTargets(firstRespondersCluster1Waypoints, "FRPOI");
        getAllTargets(threatCluster1Waypoints, "TPOI");
        getAllTargets(eventEmployeeCluster1Waypoints, "EPOI");
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetTargetTransform(NPCRole type) // we have 3 types of NPCs Civilians(0), First Responders(1) and Threats(2)
    {
        if (type == NPCRole.Threat)
        {
            return threatCluster1Waypoints[Random.Range(0, threatCluster1Waypoints.Count)];
        }
        else if (type == NPCRole.FirstResponder)
        {
            return firstRespondersCluster1Waypoints[Random.Range(0, firstRespondersCluster1Waypoints.Count)]; 
        }
        else if (type == NPCRole.Civilian)
        {
            return civCluster1Waypoints[Random.Range(0, civCluster1Waypoints.Count)];
        }
        else if (type == NPCRole.EventEmployee)
        {
            return eventEmployeeCluster1Waypoints[Random.Range(0, civCluster1Waypoints.Count)];
        }

        return null;
    }
    private void getAllTargets(List<Transform> targets, string type)
    {
        // reset waypoints
        targets.Clear();
        // find all objects with POI tag and add them to waypoints
        foreach(GameObject poi in GameObject.FindGameObjectsWithTag(type))
        {
            targets.Add(poi.transform);
        }
    }
    
    
}
