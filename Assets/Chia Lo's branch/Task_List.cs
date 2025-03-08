using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Task_List : MonoBehaviour
{
    private int sweepMode = 0;

    public TextMeshProUGUI mode;

    [SerializeField] private List<ClusterNavigator> security;

    [SerializeField] private RadiationDetection radiationDetection;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] clusterNavigators = GameObject.FindGameObjectsWithTag("Security");

        foreach (var gameObject in clusterNavigators)
        {
            ClusterNavigator navigator = gameObject.GetComponent<ClusterNavigator>();
            if (navigator != null)
            {
                security.Add(navigator);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int index = 0;
        foreach (ClusterNavigator change in security)
        {
            if (change.GetComponent<RadiationDetection>().getThreSholdReached())
            {
                Debug.LogWarning("ALERT!!! " + security[index] + " Found Radiation Detected");
            }

            index++;
        }
        switch (sweepMode)
        {
            case 0 :
                mode.text = "Patrol";
                foreach (ClusterNavigator change in security)
                {
                        change.newNPCRole(NPCRole.Patrol);
                }
                break;
            case 1 :
                mode.text = "stadium sweep start";
                foreach (ClusterNavigator change in security)
                {
                    if (change.Role() == SecurityRole.Stadium)
                    {
                        change.newNPCRole(NPCRole.Stadium);
                    }
                }
                break;
            case 2 :
                mode.text = "parking lot sweep start";
                foreach (ClusterNavigator change in security)
                {
                    if (change.Role() == SecurityRole.ParkingLot)
                    {
                        change.newNPCRole(NPCRole.ParkingLot);
                    }
                }
                break;
        }
    }

    public void changeMode(int mode)
    {
        sweepMode = mode;
    }
}
