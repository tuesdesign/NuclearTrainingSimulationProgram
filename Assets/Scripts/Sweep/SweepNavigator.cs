using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

[RequireComponent(typeof(NavMeshAgent))] // Require a NavMeshAgent component to be attached to the GameObject
public class SweepNavigator : MonoBehaviour
{
    [SerializeField] private Role npcRole; // Role to be edited in the inspector    
    [SerializeField] private int subCluster; // Sub-cluster ID to be edited in the inspector    
    [SerializeField] private SecurityRole role;
    
    [SerializeField]
    SweepManager manager; // Reference to the SweepManager script
    
    private NavMeshAgent agent; // NavMeshAgent for pathfinding
    [SerializeField]
    private bool DEBUG = false; // Debug flag
    
    private Transform target; // Current target waypoint
    private int index = 0; // Index to track current waypoint

    [SerializeField, Range(1f, 5f)]
    private float acceptableDistance = 0.1f; // Distance to the target to consider it "reached"
    
    [SerializeField, Range(0.1f, 3f)]
    private float WaitTime = 1f; // Time to wait at each waypoint
    
    [SerializeField]
    private Animator animator; // Animator for NPC animations

    void Start()
    {
        if (manager == null) manager = FindObjectOfType<SweepManager>(); // Try to find the SweepManager
        agent = GetComponent<NavMeshAgent>();
        SetNewTarget(); // Start the pathfinding process
    }

    bool IsAtTarget()
    {
        if (Vector3.Distance(transform.position, target.position) < acceptableDistance) // If the agent is close enough to the target
        {
            if (DEBUG) print("Arrived at Target");
            return true;
        }
        return false;
    }

    void SetNewTarget()
    {
        target = manager?.GetTargetTransform(npcRole, subCluster, ref index); // Get next target from SweepManager
        if (target == null)
        {
            if (DEBUG) Debug.LogWarning($"No waypoints found for {npcRole} in sub-cluster {subCluster}");
            return;
        }

        if (DEBUG) Debug.Log($"New target set for {npcRole} in sub-cluster {subCluster}: {target.name}");
        StartCoroutine(WaitAtWaypoint());
    }

    IEnumerator WaitAtWaypoint()
    {
        agent.isStopped = true; // Stop movement when waiting
        animator.SetBool("Walking", false); // Set animator state to idle
        animator.Play("Idle"); // Play idle animation
        yield return new WaitForSeconds(WaitTime); // Wait for the specified time
        agent.isStopped = false; // Resume movement
        animator.Play("Walking"); // Play walking animation
        animator.SetBool("Walking", true); // Set animator state to walking
        agent.SetDestination(target.position); // Set destination to the target waypoint
    }

    public void newRole(Role newNPCRole)
    {
        npcRole = newNPCRole;
    }

    public SecurityRole Role()
    {
        return role;
    }

    public void stop()
    {
        agent.isStopped = true;
    }

    public void go()
    {
        agent.isStopped = false;
    }

    void Update()
    {
        if (IsAtTarget()) 
        {
            SetNewTarget(); // Get new target when we arrive at the current target
        }
    }
}

public enum Role
{
    Patrol,
    Stadium,
    ParkingLot
}

public enum SecurityRole
{
    Patrol,
    Stadium,
    ParkingLot
}