using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public Camera cam;
    public KeyCode interactKey;
    public float maxReach = 1.0f;

    // Checks whether a script is derived from the InteractiveObject class	
    private bool IsObjectInteractive(GameObject obj)
    {
        foreach (MonoBehaviour script in obj.GetComponents<MonoBehaviour>())
        {
            if (script.GetType().IsSubclassOf(typeof(InteractiveObject)))
            {
                return true;
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if (hit.distance <= maxReach && IsObjectInteractive(objectHit.gameObject))
                {
                    InteractiveObject actionObject = (InteractiveObject)objectHit.GetComponent(typeof(InteractiveObject));
                    actionObject.Action();
                }
            }
        }
    }
}

