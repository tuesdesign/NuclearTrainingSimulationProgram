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
        switch (sweepMode)
        {
            case 0 :
                mode.text = "Patrol";
                foreach (SweepNavigator change in security)
                {
                        change.newRole(Role.Patrol);
                }
                break;
            case 1 :
                mode.text = "Stadium Sweep";
                foreach (SweepNavigator change in security)
                {
                    if (change.Role() == SecurityRole.Stadium)
                    {
                        change.newRole(Role.Stadium);
                    }
                }
                break;
            case 2 :
                mode.text = "Parking Lot Sweep";
                foreach (SweepNavigator change in security)
                {
                    if (change.Role() == SecurityRole.ParkingLot)
                    {
                        change.newRole(Role.ParkingLot);
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
