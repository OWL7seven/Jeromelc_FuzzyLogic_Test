/*
 * This code is part of Jerome Chetty Summision for ITTYHNK GameJam 2021
 */
using UnityEngine;
using UnityEngine.AI;

// Walk to a random position and repeat
[RequireComponent(typeof(NavMeshAgent))]
public class RandomWalk : MonoBehaviour
{
    [SerializeField]
    private float range = 25.0f;
    [SerializeField]
    public float minDistance = 0.1f;
    [SerializeField]
    public bool randomLocation = false;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private Vector3 finalPosition;
    [SerializeField]
    private float targetdistance;
    [SerializeField]
    private bool atTarget;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (randomLocation)
        {
            transform.position = RandomNavmeshLocation(range);
        }
        GameManager.onInvokedUpdate += Walk;
    }

    private void Walk()
    {
        if (agent != null)
        {
            if (agent.isOnNavMesh)
            {
                if (agent.pathPending || agent.remainingDistance > minDistance)
                {
                    return;
                }

                targetPosition = agent.destination;
                targetdistance = agent.remainingDistance;
                agent.destination = RandomNavmeshLocation(range);
            }
        }
    }
    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
