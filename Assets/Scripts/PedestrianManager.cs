/*
 * This code is part of Jerome Chetty Summision for FuzzyLogic first stage 2021
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PedestrianManager : MonoBehaviour
{
    //Gobal reference to access instance of this class
    public static PedestrianManager Instance;

    //instantiate this class as gobal instance
    private void Awake()
    {
        Instance = this;
    }

    //Number of pedestrians to spawn
    [SerializeField]
    private int numberOfPeds = 10;

    //list of active pedestrians
    [SerializeField]
    private List<NavMeshAgent> agents = new List<NavMeshAgent>();

    //list of pedestrians spawn points
    [SerializeField]
    private List<Transform> spawnPoints = new List<Transform>();

    [Header("Debug")]
    //Random spawning of pedestrians
    [SerializeField]
    private bool randomSpawn;

    //Number of predestrian prefabs found in the resources
    //Hard coded for now
    //Will create a search fucntion that will check for current amount
    [SerializeField]
    private int numberOfPedPrefabs = 3;

    [SerializeField]
    private NavMeshSurface navMesh;

    private void Start()
    {
        if (randomSpawn)
        {
            SpawnRandomPositions();
        }
        else
        {
            foreach (Transform spawn in gameObject.GetComponentsInChildren<Transform>())
            {
                if (spawn.tag == "Spawn")
                {
                    spawnPoints.Add(spawn);
                }
            }
            SpawnLocations();
        }

        AssignUi();
    }

    private void AssignUi()
    {
        UIManager.Instance.pedestriansInputField.onValueChanged.AddListener(delegate { ValueChange(int.Parse(UIManager.Instance.pedestriansInputField.text)); });
        UIManager.Instance.pedestriansInputField.placeholder.gameObject.GetComponent<TextMeshProUGUI>().text = numberOfPeds.ToString();
    }

    // Invoked when the value of the text field changes.
    public void ValueChange(int value)
    {
        numberOfPeds = value;
    }

    //Spawns peds in random locations of the nav mesh
    private void SpawnRandomPositions()
    {
        for (int i = 0; i < numberOfPeds; i++)
        {
            NavMeshAgent agent = Instantiate(Resources.Load<NavMeshAgent>($"Prefabs/Pedestrians/Passenger_0{Random.Range(1, numberOfPedPrefabs + 1)}"));
            NavMeshHit hit;
            agent.transform.position = RandomNavmeshLocation(RoadManager.Instance.GetSize());
            RandomWalk walk = agent.gameObject.AddComponent<RandomWalk>();
            walk.randomLocation = randomSpawn;
            walk.minDistance = 1;
            walk.randomLocation = false;

            Pedestrian ped = agent.gameObject.AddComponent<Pedestrian>();
            ped.agent = agent;
            ped.randomWalk = walk;
            agents.Add(agent);
        }
    }

    //Spawns peds at transform locations create in the children of this gamobject
    private void SpawnLocations()
    {
        foreach (Transform spawn in spawnPoints)
        {
            for (int i = 0; i < numberOfPeds; i++)
            {
                AddPedestrian(spawn);
            }
        }
    }
    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    //Create Pedestiran
    private void AddPedestrian(Transform spawn)
    {
        int randomNumber = Random.Range(1, numberOfPedPrefabs + 1);

        NavMeshAgent agent = Instantiate(Resources.Load<NavMeshAgent>($"Prefabs/Pedestrians/Passenger_0{randomNumber}"));
        agent.transform.position = spawn.transform.position;
        //agent.destination = spawn.transform.position;

        RandomWalk walk = agent.gameObject.AddComponent<RandomWalk>();
        walk.minDistance = 1;
        walk.randomLocation = false;

        Pedestrian ped = agent.gameObject.AddComponent<Pedestrian>();
        ped.agent = agent;
        ped.randomWalk = walk;

        agents.Add(agent);
    }

    public void Respawn()
    {
        foreach (var item in agents)
        {
            GameManager.onFixedUpdate -= item.gameObject.GetComponent<Pedestrian>().PedestrianFixedUpdate;
            Destroy(item.gameObject);
        }
        agents.Clear();
        if (randomSpawn)
        {
            SpawnRandomPositions();
        }
        else
        {
            SpawnLocations();
        }
    }
}
// used to find random location on the nav mesh to spawn the pedestrians
public static class NavMeshUtil
{

    // Get Random Point on a Navmesh surface
    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit; // NavMesh Sampling Info Container

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }

}