using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task_List : MonoBehaviour
{
    private int sweepMode = 0;

    public TextMeshProUGUI mode;
	private bool arrest;

    [SerializeField] private List<ClusterNavigator> security;
    // Start is called before the first frame update
    void Start()
    {
        ClusterNavigator[] clusterNavigators = FindObjectsOfType<ClusterNavigator>();

        foreach (var navigator in clusterNavigators)
        {
            security.Add(navigator); // Add each found ClusterNavigator to the list
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (sweepMode)
        {
            case 0 :
                mode.text = "Patrol";
                foreach (ClusterNavigator change in security)
                {
                    if (change.Role() == SecurityRole.Patrol)
                    {
                        change.newSubCluster(0);
                    }
                }
                break;
            case 1 :
                mode.text = "stadium sweep start";
                foreach (ClusterNavigator change in security)
                {
                    if (change.Role() == SecurityRole.ParkingLot)
                    {
                        change.newSubCluster(1);
                    }
                }
                break;
            case 2 :
                mode.text = "parking lot sweep start";
                foreach (ClusterNavigator change in security)
                {
                    if (change.Role() == SecurityRole.ParkingLot)
                    {
                        change.newSubCluster(2);
                    }
                }
                break;
			case 3:
                mode.text = "ALERT!!! Radiation Detected";
                foreach (ClusterNavigator change in security)
                {
                    change.newSubCluster(3);
                }
                break;
        }
    }

    public void changeMode(int mode)
    {
        sweepMode = mode;
    }
}
