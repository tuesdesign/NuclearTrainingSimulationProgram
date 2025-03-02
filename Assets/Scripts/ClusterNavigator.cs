// Manvir Punglia 
// ClusterNavigator.cs
// This script is a further development of the PersonNavigator.cs script  

// Note: this is for a research project and might need to be changed by someone else later, i'll comment everything to a silly degree. 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


[RequireComponent(typeof(NavMeshAgent))] // Require a NavMeshAgent component to be attached to the GameObject

public class ClusterNavigator : MonoBehaviour
{
    [SerializeField] private NPCRole npcRole;// a changeable enum to be edited in the inspector    
    [SerializeField] private int subCluster;// a changeable enum to be edited in the inspector    
    [SerializeField] private SecurityRole role;
    
    [SerializeField]
    ClusterManager clusterManager;// required to get POI location 
    
    NavMeshAgent agent; // Required component for pathfinding
    [SerializeField]
    private bool DEBUG = false; // Determines if debug messages are printed to the console

    [SerializeField]
    private List<Transform> waypoints; // A list of all possible waypoints to target
    
    [SerializeField, Range(1f, 5f)]
    private float acceptableDistance = 0.1f; // The distance at which the agent is considered to have reached the target

    [SerializeField, Range(0.1f, 3f)]
    private float WaitTime = 1f; // The time the agent waits at a waypoint before moving to the next one

    [SerializeField]
    private Animator animator; // The animator component for the agent

    //[SerializeField]
    //private AnimationClip IdleAnimation; // The animator component for the agent

    //[SerializeField]
    //private AnimationClip WalkAnimation; // The animator component for the agent

    private Transform target; // The current target waypoint
    private bool detected = false;

    void Start()
    {
        if(clusterManager == null) clusterManager = FindObjectOfType<ClusterManager>(); 
        agent = GetComponent<NavMeshAgent>();
        SetNewTarget();// starts the pathfinding process 
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
        target = clusterManager?.GetTargetTransform(npcRole, subCluster);
        
        if (target == null)
        {
            if (DEBUG) Debug.LogWarning($"No waypoints found for {npcRole} in sub-cluster {subCluster}");
            return;
        }

        if (DEBUG) Debug.Log($"New target set for {npcRole} in sub-cluster {subCluster}: {target.name}");
        StartCoroutine(WaitAtWaypoint());
    }

    public void isDetected(bool newDetected)
    {
        detected = newDetected;
    }

    public bool getDetected()
    {
        return detected;
    }

    IEnumerator WaitAtWaypoint()
    {
        agent.isStopped = true; // Stop
        animator.SetBool("Walking", false); // Set the animator to not walking
        //animator.Play("Idle"); // Play the idle animation
        yield return new WaitForSeconds(WaitTime); // Wait
        agent.isStopped = false; // Start
        //animator.Play("Walking"); // Play the walk animation
        animator.SetBool("Walking", true); // Set the animator to walking
        agent.SetDestination(target.position); // Then, set the agent's destination to it
    }

    public void newSubCluster(int newsubCluster)
    {
        subCluster = newsubCluster;
    }

    public SecurityRole Role()
    {
        return role;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAtTarget()) SetNewTarget(); // This one's just a sentence.
    }
}

public enum NPCRole // this is an enum setup so that developers can change the behaviour of a npc using just the inspector 
{
    Threat,
    FirstResponder,
    Civilian,
    EventEmployee,
    Security
}

public enum SecurityRole
{
    Patrol,
    Stadium,
    ParkingLot,
    Radioactive
}