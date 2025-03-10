using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Task_List : MonoBehaviour
{
    private int sweepMode = 0;

    public TextMeshProUGUI mode;

    [SerializeField] private List<SweepNavigator> security;

    [SerializeField] private RadiationDetection radiationDetection;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] clusterNavigators = GameObject.FindGameObjectsWithTag("Security");

        foreach (var gameObject in clusterNavigators)
        {
            SweepNavigator navigator = gameObject.GetComponent<SweepNavigator>();
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
        foreach (SweepNavigator change in security)
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
                foreach (SweepNavigator change in security)
                {
                        change.newNPCRole(Role.Patrol);
                }
                break;
            case 1 :
                mode.text = "stadium sweep start";
                foreach (SweepNavigator change in security)
                {
                    if (change.Role() == SecurityRole.Stadium)
                    {
                        change.newNPCRole(Role.Stadium);
                    }
                }
                break;
            case 2 :
                mode.text = "parking lot sweep start";
                foreach (SweepNavigator change in security)
                {
                    if (change.Role() == SecurityRole.ParkingLot)
                    {
                        change.newNPCRole(Role.ParkingLot);
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
