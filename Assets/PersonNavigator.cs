// Constantine Pallas
// PersonNavigator.cs
// Control the movement of a simulated person using a NavMeshAgent component

// Note: this is for a research project and might need to be changed by someone else later, i'll comment everything to a silly degree. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

[RequireComponent(typeof(NavMeshAgent))] // Require a NavMeshAgent component to be attached to the GameObject
public class PersonNavigator : MonoBehaviour
{
    NavMeshAgent agent; // Required component for pathfinding
    [SerializeField]
    private bool DEBUG = false; // Determines if debug messages are printed to the console

    [SerializeField]
    private List<Transform> waypoints; // A list of all possible waypoints to target
    
    [SerializeField, Range(1f, 5f)]
    private float acceptableDistance = 0.1f; // The distance at which the agent is considered to have reached the target

    [SerializeField, Range(0.1f, 3f)]
    private float WaitTime = 1f; // The time the agent waits at a waypoint before moving to the next one

    private Transform target; // The current target waypoint


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewTarget();
    }

    Transform GetRandomWaypoint()
    {
        if (DEBUG) print("Getting random waypoint");
        return waypoints[Random.Range(0, waypoints.Count)]; // Return a random waypoint from the list
    }

    bool IsAtTarget()
    {
        if (Vector3.Distance(transform.position, target.position) < acceptableDistance) // Return true if the agent is within an acceptable distance of the target
        {
            if (DEBUG) print("Arrived at Target");
            return true;
        } else return false;
    }

    void SetNewTarget()
    {
        target = GetRandomWaypoint(); // Get a new target
        StartCoroutine(WaitAtWaypoint()); // Start the coroutine to wait at the waypoint
    }

    IEnumerator WaitAtWaypoint()
    {
        agent.isStopped = true; // Stop
        yield return new WaitForSeconds(WaitTime); // Wait
        agent.isStopped = false; // Start
        agent.SetDestination(target.position); // Then, set the agent's destination to it
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAtTarget()) SetNewTarget(); // This one's just a sentence.
    }
}
