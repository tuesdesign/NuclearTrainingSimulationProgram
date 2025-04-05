using UnityEngine;
using UnityEngine.AI;

public class customeRotation : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y, 0);
    }
}