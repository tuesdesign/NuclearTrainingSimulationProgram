using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestBehaviour : MonoBehaviour
{
    public enum pointOfInterestType
    {
        standard,
        pickup,
        dropoff
    }

    public pointOfInterestType poiType = pointOfInterestType.standard;

    public virtual Transform GetNavTarget(PersonNavigator person)
    {
        return transform;
    }
}
