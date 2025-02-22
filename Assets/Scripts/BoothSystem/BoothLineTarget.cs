using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothLineTarget : MonoBehaviour
{
    [SerializeField] BoothManager parentBoothManager;
    public PersonNavigator assignedPerson;
    [SerializeField] bool canServe;
    [SerializeField, Range(1.25f, 5f)] float servingDistance;
    public float servingTime;
    public float servingTimeLeft;
    bool serving;


    public void SetParentBoothManager(BoothManager boothManager)
    {
        parentBoothManager = boothManager;
    }

    public void SetCanServe(bool can)
    {
        canServe = can;
    }

    private void Update()
    {
        //If the assigned person on the target is within range
        if (assignedPerson != null)
        {
            if (!serving && Vector3.Distance(assignedPerson.transform.position, transform.position) <= servingDistance)
            {
                StartCoroutine(ServePerson());
            }
        }
    }

    IEnumerator ServePerson()
    {
        serving = true;
        yield return new WaitForSeconds(servingTime);
        assignedPerson.SetNewTarget();
        parentBoothManager.RemovePersonInLine(assignedPerson);
        serving = false;

    }
    //If assigned person in range start timer
    //if timer 0
    //remove person from line
    //make person go somewhere else



}
