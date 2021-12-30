/*
 * This code is part of Jerome Chetty Summision for FuzzyLogic first stage 2021
 */
using UnityEngine.AI;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    // agent attach to gameobject
    public NavMeshAgent agent;
    // randomWalk attach to gameobject
    public RandomWalk randomWalk;

    //Add fixed update logic to gamemanager
    private void Start()
    {
        GameManager.onFixedUpdate += PedestrianFixedUpdate;
    }

    //Raycast variables
    [SerializeField]
    private Vector3 rayDirection = new Vector3(0, -0.5f, 1);
    [SerializeField]
    private Vector3 rayOffset = new Vector3(0, 0.5f, 0f);
    [SerializeField]
    private float rayDistance = 10;
    [SerializeField]
    private bool debug;
    [SerializeField]
    private bool hitActive;

    //Ground based objects
    [SerializeField]
    private Road currentRoad;
    [SerializeField]
    private GameObject currentObj;

    public void PedestrianFixedUpdate()
    {
        //Shoot ray out fowrard of object
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayOffset, transform.TransformDirection(rayDirection), out hit, rayDistance))
        {
            hitActive = true;
            if (debug)
            {
                Debug.DrawRay(transform.position + rayOffset, transform.TransformDirection(rayDirection) * hit.distance, Color.yellow);
            }
            //if the object is not the current road, remove the pedestrain from the current road
            if (hit.collider.gameObject != null)
            {
                currentObj = hit.collider.gameObject;
                if (currentObj.GetComponent<Road>() != null)
                {
                    if (currentObj.GetComponent<Road>() != currentRoad)
                    {
                        if (currentRoad != null)
                        {
                            currentRoad.AddPed(this, false);
                            currentRoad = null;
                        }
                    }
                }
            }
            //if no road has been hit and a new one is found, then assign new road to pedestrian
            if (hit.collider.gameObject.GetComponent<Road>() != null)
            {
                if (currentRoad == null)
                {
                    currentRoad = hit.collider.gameObject.GetComponent<Road>();
                    currentRoad.AddPed(this, true);
                }
            }           
        }
    }
}
