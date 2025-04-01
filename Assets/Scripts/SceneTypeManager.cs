using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneTypeManager : MonoBehaviour
{
    [SerializeField] PointOfInterestBehaviour[] pointsOfInterest;
    public enum SceneType
    {
        Setup,
        Opening,
        Event,
        Closing

    }

    [SerializeField] SceneType sceneType;

    public SceneType GetSceneType() { return sceneType; }

    private void Start()
    {
        //If the current scene is a setup for the event
        if(sceneType == SceneType.Setup)
        {
            pointsOfInterest = FindObjectsOfType<PointOfInterestBehaviour>();

            bool thereIsValidDropOff = false;

            // Check if there is a valid drop off point in the scene
            foreach (PointOfInterestBehaviour pointOfInterest in pointsOfInterest) { 
                if(pointOfInterest.poiType == PointOfInterestBehaviour.pointOfInterestType.dropoff)
                {
                    thereIsValidDropOff = true;
                }
            }
            //If there is no valid drop off point
            if (!thereIsValidDropOff) {
                Debug.LogWarning("Play prevented. There is no valid drop off point in the scene. Please add a POI object with type drop off or change the scene out of setup type");
                //Stop Play Mode
                EditorApplication.isPlaying = false;
            }

        }
    }
}


