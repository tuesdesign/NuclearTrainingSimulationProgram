// Manvir Punglia 
// This script manages waypoints organized by NPC roles and sub-clusters, providing random targets for NPC navigation.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClusterManager : MonoBehaviour
{
    //Represents a subgroup of waypoints within a larger role cluster. Allows for hierarchical organization of navigation points.
    [Serializable]
    public class SubCluster
    {
        public int subClusterID;
        public List<Transform> waypoints;
    }
    //Contains all sub-clusters associated with a specific NPC role type, Enemies, civilians, etc., would each have their own cluster
    [Serializable]
    public class Cluster
    {
        public NPCRole role;
        public List<SubCluster> subClusters;
    }
    
    // Inspector-accessible list of role-based clusters, so you can fill them in the inspector
    [SerializeField] private List<Cluster> clusters;
    
    // on runtime the list gets coppied to this dictionary 
    private Dictionary<NPCRole, Dictionary<int, List<Transform>>> clusterWaypoints = new();
    
    private void Awake()
    {
        LoadWaypoints();
    }
    
    // Retrieves a random waypoint for the specified NPC role and sub-cluster
    public Transform GetTargetTransform(NPCRole type, int subCluster)
    {
        if (clusterWaypoints.TryGetValue(type, out var subClusters) && subClusters.TryGetValue(subCluster, out var waypoints))
        {
            return waypoints.Count > 0 ? waypoints[Random.Range(0, waypoints.Count)] : null;
        }
        return null;
    }
    
    // Converts inspector-assigned cluster data into a dictionary 
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

