// Constantine Pallas
// PersonNavigator.cs
// Control the movement of a simulated person using a NavMeshAgent component

// Note: this is for a research project and might need to be changed by someone else later, i'll comment everything to a silly degree. 

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; 


[RequireComponent(typeof(NavMeshAgent))] // Require a NavMeshAgent component to be attached to the GameObject
public class PersonNavigator : MonoBehaviour
{
    protected NavMeshAgent agent; // Required component for pathfinding
    [SerializeField]
    protected bool DEBUG = false; // Determines if debug messages are printed to the console

    [SerializeField]
    protected List<PointOfInterestBehaviour> waypoints; // A list of all possible waypoints to target

    [SerializeField, Range(0.5f, 5f)]
    protected float acceptableDistance = 0.5f; // The distance at which the agent is considered to have reached the target

    [SerializeField, Range(0.1f, 3f)]
    protected float WaitTime = 1f; // The time the agent waits at a waypoint before moving to the next one

    public bool waiting;

    [SerializeField]
    protected Animator animator; // The animator component for the agent

    //[SerializeField]
    //private AnimationClip IdleAnimation; // The animator component for the agent

    //[SerializeField]
    //private AnimationClip WalkAnimation; // The animator component for the agent

    [SerializeField] protected Transform target; // The current target waypoint
    [SerializeField, Range(1,100)] protected int maxNumberOfTargetFindTries;

    [SerializeField] string waypointTag = "POI";

    [SerializeField] protected SceneTypeManager sceneTypeManager;
    [SerializeField] protected SceneTypeManager.SceneType currentSceneType;

    [SerializeField] PointOfInterestBehaviour.pointOfInterestType currentTargetType;

    public virtual void Awake()
    {
        //Get Scene Manager
        sceneTypeManager = FindObjectOfType<SceneTypeManager>();
        UpdateSceneType();

        //If in a setup scene
        if (currentSceneType == SceneTypeManager.SceneType.Setup)
        {
            ChangeTargetType(PointOfInterestBehaviour.pointOfInterestType.dropoff);
        }

        agent = GetComponent<NavMeshAgent>();
        getAllTargets();
        SetNewTarget();

    }

    PointOfInterestBehaviour GetRandomWaypoint()
    {
        //getAllTargets();
        if (DEBUG) print("Getting random waypoint");
        return waypoints[Random.Range(0, waypoints.Count - 1)]; // Return a random waypoint from the list
    }

    public void getAllTargets()
    {
        // reset waypoints
        waypoints.Clear();
        // find all objects with POI tag and add them to waypoints
        foreach(GameObject poi in GameObject.FindGameObjectsWithTag(waypointTag))
        {
            waypoints.Add(poi.GetComponent<PointOfInterestBehaviour>());
        }
    }

    protected bool IsAtTarget()
    {
        //If there is no target find a target
        if (target == null)
        {
            SetNewTarget();
        }

        if (Vector3.Distance(transform.position, target.position) < acceptableDistance) // Return true if the agent is within an acceptable distance of the target
        {
            if (DEBUG) print("Arrived at Target");
            return true;
        } else return false;
    }

    public void SetNewTarget()
    {
        
        

        PointOfInterestBehaviour waypoint = GetRandomWaypoint();


        target = waypoint.GetNavTarget(this); // Get a new target

        //If there isn't a target or target doesn't match current behaviour find a new one
            
        if (waypoint.poiType != currentTargetType || target == null)
        {
            SetNewTarget();
            return;
        }



        

        StartCoroutine("WaitAtWaypoint"); // Start the coroutine to wait at the waypoint

    }

    public void SetNewTarget(Transform trgt)
    {
        target = trgt;
        StartCoroutine("WaitAtWaypoint");
    }

    protected virtual IEnumerator WaitAtWaypoint()
    {
        
        yield return new WaitForSeconds(WaitTime); // Wait
        agent.isStopped = false; // Start
        //animator.Play("Walking"); // Play the walk animation
        animator.SetBool("Walking", true); // Set the animator to walking
        agent.SetDestination(target.position); // Then, set the agent's destination to it
        
    }

    public void SetTargetDestination()
    {
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        

        //If the person is at their desired destination
        if (IsAtTarget())
        {
            agent.isStopped = true; // Stop
            animator.SetBool("Walking", false); // Set the animator to not walking
            //animator.Play("Idle"); // Play the idle animation
            if (!waiting)
            {
                //If person was dropping off, go back to picking up from vehicles.
                if(currentTargetType == PointOfInterestBehaviour.pointOfInterestType.dropoff)
                {
                    if(sceneTypeManager.IsThereOfType(PointOfInterestBehaviour.pointOfInterestType.pickup)) ChangeTargetType(PointOfInterestBehaviour.pointOfInterestType.pickup);
                    else ChangeTargetType(PointOfInterestBehaviour.pointOfInterestType.standard);
                }
                //If person was picking up from a car, find a place to drop off
                else if (currentTargetType == PointOfInterestBehaviour.pointOfInterestType.pickup)
                {
                    ChangeTargetType(PointOfInterestBehaviour.pointOfInterestType.dropoff);
                }


                SetNewTarget(); // This one's just a sentence.
            }
        }

        

    }


    protected virtual void UpdateSceneType()
    {
        currentSceneType = sceneTypeManager.GetSceneType();
    }

    protected virtual void ChangeTargetType(PointOfInterestBehaviour.pointOfInterestType type)
    {
        currentTargetType = type;
    }
}
