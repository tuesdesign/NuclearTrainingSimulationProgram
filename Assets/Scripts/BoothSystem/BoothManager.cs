using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoothManager : PointOfInterestBehaviour
{
    [SerializeField] 
    List<BoothLineTarget> boothLineTargets;

    [SerializeField] 
    List<PersonNavigator> personsInLine = new List<PersonNavigator>();

    [SerializeField] 
    GameObject queueTargetPrefab;

    [SerializeField, Tooltip("How long until a person is served.")]
    float boothServiceTime;

    [SerializeField, Tooltip("How many people can be served at the same time. Can't be set higher than the number of boothLineTargets")]
    int boothServiceCapacity; 

    void Start()
    {
        //Update Line
        UpdateBoothLine();


        //If there is no line create one at the manager's location
        if (boothLineTargets.Count == 0)
        {
            AddBoothLineTarget();
        }

        
    }

    
    public void AddBoothLineTarget()
    {
        //Add a target to scene as a child of the manager
        Instantiate(queueTargetPrefab, transform.position, transform.rotation, transform);
    }

    public void UpdateBoothLine()
    {

        //Get all children that have the "BoothlineTarget" component
        boothLineTargets = new List<BoothLineTarget>();
        boothLineTargets.Clear();
        foreach (Transform target in transform) {

            BoothLineTarget boothTarget = target.gameObject.GetComponent<BoothLineTarget>();

            //Add booth line target to the list
            boothLineTargets.Add(boothTarget);
            //Reference this object to the target
            boothTarget.SetParentBoothManager(this);
            //Reset bool values for the targets
            boothTarget.SetCanServe(false);
            boothTarget.servingTime = boothServiceTime;

        }

        for (int i = 0; i < boothServiceCapacity; i++)
        {
            //Set targets that can serve/be used by people.
            boothLineTargets[i].SetCanServe(true);

        }

        //Force the first booth line target to be able to serve
        boothLineTargets[0].canServe = true;

    }


    
    public override Transform GetNavTarget(PersonNavigator person)
    {
        
        //Check If line isn't full
        if (personsInLine.Count < boothLineTargets.Count)
        {
            //Turn on waiting preventing the person from wandering when lined up.
            person.waiting = true;
            //Add the person to the line
            personsInLine.Add(person);
            //Update All LineTargets
            UpdateLine();
            //Gives the end of the line
            return boothLineTargets[personsInLine.Count - 1].transform;



        } else
        {

            //Choose something else
            return null;
        }

    }

    private void UpdateLine()
    {
        //Foreach person in line
        for (int i = 0; i < personsInLine.Count; i++)
        {
            //Give reference of the person to the target
            boothLineTargets[i].assignedPerson = personsInLine[i];
            //Order each person to go to their new targets
            personsInLine[i].SetNewTarget(boothLineTargets[i].transform);

        }
    }
    public void RemovePersonInLine(PersonNavigator person)
    {
        personsInLine.Remove(person);
        UpdateLine();
    }

}
