using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarNavigator : PersonNavigator
{
    [SerializeField] GameObject peoplePrefab;
    [SerializeField] int numberOfPeople;
    [SerializeField] GameObject carPOI;

    [SerializeField] Transform peopleSpawner; //Location where people would exit the vehicle
    public override void Awake()
    {
        sceneTypeManager = FindObjectOfType<SceneTypeManager>();
        UpdateSceneType();

        //Car POI should always be standard
        ChangeTargetType(PointOfInterestBehaviour.pointOfInterestType.standard);

        agent = GetComponent<NavMeshAgent>();
        getAllTargets();
        SetNewTarget();
    }

    protected override void Update()
    {
        if (target != null)
        {
            if (IsAtTarget())
            {
                Debug.Log("HERE");
                //Set Rotation
                transform.rotation = target.transform.rotation;
                //Reset target
                target = null;
                //Disable Navigation Agent
                transform.GetComponent<NavMeshAgent>().enabled = false;
                
                
                //Spawn people
                SpawnPeople();

                if(currentSceneType == SceneTypeManager.SceneType.Setup)
                {
                    //Turn on pickup POI
                    carPOI.SetActive(true);
                }


            }
        }
    }

    protected override IEnumerator WaitAtWaypoint()
    {
        yield return new WaitForSeconds(0); // Wait
        agent.SetDestination(target.position); //Set Destination
        target.gameObject.SetActive(false); //Deactivate target to prevent other cars from taking it
        
    }

    void SpawnPeople()
    {
        for (int i = 0; i < numberOfPeople; i++) { 
        
            Instantiate(peoplePrefab, peopleSpawner.position, peopleSpawner.rotation);
        
        }

    }
}
