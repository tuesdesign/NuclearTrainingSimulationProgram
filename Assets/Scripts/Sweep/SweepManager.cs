using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SweepManager : MonoBehaviour
{
    [Serializable]
    public class SubCluster
    {
        public int subClusterID;
        public List<Transform> waypoints;
    }
    
    [Serializable]
    public class Cluster
    {
        public Role role;
        public List<SubCluster> subClusters;
    }

    [SerializeField] private List<Cluster> clusters;
    private Dictionary<Role, Dictionary<int, List<Transform>>> clusterWaypoints = new();
    
    // New member to keep track of the current index for each subcluster
    private Dictionary<Role, Dictionary<int, int>> subClusterWaypointIndices = new();
    
    private void Awake()
    {
        LoadWaypoints();
    }
    
    public Transform GetTargetTransform(Role type, int subCluster)
    {
        if (clusterWaypoints.TryGetValue(type, out var subClusters) && subClusters.TryGetValue(subCluster, out var waypoints))
        {
            // Check if there is a way to get the next waypoint in order
            if (!subClusterWaypointIndices.ContainsKey(type))
            {
                subClusterWaypointIndices[type] = new Dictionary<int, int>();
            }
            
            if (!subClusterWaypointIndices[type].ContainsKey(subCluster))
            {
                subClusterWaypointIndices[type][subCluster] = 0;  // Start from the first waypoint
            }

            int currentIndex = subClusterWaypointIndices[type][subCluster];
            Transform targetWaypoint = waypoints[currentIndex];
            
            // Move to the next waypoint in order, looping back to the first one when done
            currentIndex = (currentIndex + 1) % waypoints.Count;
            subClusterWaypointIndices[type][subCluster] = currentIndex;
            
            return targetWaypoint;
        }
        return null;
    }
    
    private void LoadWaypoints()
    {
        foreach (var cluster in clusters)
        {
            if (!clusterWaypoints.ContainsKey(cluster.role))
            {
                clusterWaypoints[cluster.role] = new Dictionary<int, List<Transform>>();
            }
            
            foreach (var subCluster in cluster.subClusters)
            {
                clusterWaypoints[cluster.role][subCluster.subClusterID] = new List<Transform>(subCluster.waypoints);
            }
        }
    }
}
