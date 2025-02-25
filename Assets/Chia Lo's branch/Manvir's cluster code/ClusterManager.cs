using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClusterManager : MonoBehaviour
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
        public NPCRole role;
        public List<SubCluster> subClusters;
    }

    [SerializeField] private List<Cluster> clusters;
    private Dictionary<NPCRole, Dictionary<int, List<Transform>>> clusterWaypoints = new();

    private void Awake()
    {
        LoadWaypoints();
    }

    public Transform GetTargetTransform(NPCRole type, int subCluster)
    {
        if (clusterWaypoints.TryGetValue(type, out var subClusters) &&
            subClusters.TryGetValue(subCluster, out var waypoints))
        {
            return waypoints.Count > 0 ? waypoints[Random.Range(0, waypoints.Count)] : null;
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