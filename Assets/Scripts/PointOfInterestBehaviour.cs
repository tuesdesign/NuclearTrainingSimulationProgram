using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestBehaviour : MonoBehaviour
{
    
    public virtual Transform GetNavTarget(PersonNavigator person)
    {
        Debug.Log("Get Nav Target POI");
        return transform;
    }
}
