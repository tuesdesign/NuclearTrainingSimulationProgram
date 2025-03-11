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

    private void Awake()
    {
        LoadWaypoints();
    }

    public Transform GetTargetTransform(Role type, int subCluster, ref int index)
    {
        if (clusterWaypoints.TryGetValue(type, out var subClusters) &&
            subClusters.TryGetValue(subCluster, out var waypoints))
        {
            if (waypoints.Count == 0)
            {
                return null; // No waypoints available
            }

            index = (index + 1) % waypoints.Count; // Automatically cycle the index

            return waypoints[index];
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