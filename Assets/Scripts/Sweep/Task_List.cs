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
        GameObject[] vans = GameObject.FindGameObjectsWithTag("Van");

        // Add security objects to the list
        foreach (var gameObject in clusterNavigators)
        {
            SweepNavigator navigator = gameObject.GetComponent<SweepNavigator>();
            if (navigator != null)
            {
                security.Add(navigator);
            }
        }

        // Add Van objects to the list
        foreach (var van in vans)
        {
            SweepNavigator vanNavigator = van.GetComponent<SweepNavigator>();
            if (vanNavigator != null)
            {
                security.Add(vanNavigator);
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
                    if (change.Role() != SecurityRole.Patrol)
                    {
                        if (change.CompareTag("Van"))
                        {
                            change.transform.rotation = Quaternion.Euler(-90, 0, 0);
                        }
                        else
                        {
                            change.transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        change.stop();
                    }
                }

                break;
            case 1 :
                mode.text = "Stadium Sweep";
                foreach (SweepNavigator change in security)
                {
                    if (change.Role() == SecurityRole.Stadium)
                    {
                        change.newRole(Role.Stadium);
                        change.go();
                    }

                    if (change.CompareTag("Van"))
                    {
                        VanTurn(change);
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
                        change.go();
                    }

                    if (change.CompareTag("Van"))
                    {
                        VanTurn(change);
                    }
                }
                break;
        }
    }

    private void VanTurn(SweepNavigator navigator)
    {
        NavMeshAgent agent = navigator.GetComponent<NavMeshAgent>();

        if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            Vector3 targetDirection = agent.steeringTarget - agent.transform.position;

            if (targetDirection.sqrMagnitude > 0.0f)
            {
                // Set rotation directly towards the destination while keeping the X-axis fixed to -90
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                targetRotation = Quaternion.Euler(-90, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

                // Set the rotation hard to the calculated target rotation
                agent.transform.rotation = targetRotation;
            }
        }
    }
    public void changeMode(int mode)
    {
        sweepMode = mode;
    }
}
