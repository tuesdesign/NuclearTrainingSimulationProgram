using UnityEngine;
using UnityEngine.AI;

public class customeRotation : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Disable automatic rotation
    }

    void Update()
    {
        // Your custom rotation logic here, e.g., just keeping the object facing forward
        // Or not rotating at all if you don't want any rotation
        transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);
    }
}