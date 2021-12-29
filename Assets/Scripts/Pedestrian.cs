/*
 * This code is part of Jerome Chetty Summision for ITTYHNK GameJam 2021
 */
using UnityEngine.AI;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{

    // agent attach to gameobject
    public NavMeshAgent agent;
    // animator attach to gameobject
    public Animator animator;
    // randomWalk attach to gameobject
    public RandomWalk randomWalk;

    private void Start()
    {
        GameManager.onFixedUpdate += PedestrianFixedUpdate;
    }

    [SerializeField]
    private Vector3 rayDirection = new Vector3(0, -0.5f, 1);
    [SerializeField]
    private Vector3 rayOffset = new Vector3(0, 0.5f, 0f);
    [SerializeField]
    private float rayDistance = 10;
    [SerializeField]
    private bool debug;
    [SerializeField]
    private Road currentRoad;
    public void PedestrianFixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayOffset, transform.TransformDirection(rayDirection), out hit, rayDistance))
        {
            if (debug)
            {
                Debug.DrawRay(transform.position + rayOffset, transform.TransformDirection(rayDirection) * hit.distance, Color.yellow);
            }
            if (hit.collider.gameObject.GetComponent<Road>() != null)
            {
                if (currentRoad == null)
                {
                    currentRoad = hit.collider.gameObject.GetComponent<Road>();
                    currentRoad.AddPed(this, true);
                }
            }
        }
        else 
        {
            if (currentRoad != null)
            {
                currentRoad.AddPed(this, false);
                currentRoad = null;
            }
        }
    }
}
