using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public static CarManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private List<Car> cars = new List<Car>();
    [SerializeField]
    private int spawnCount = 1;
    [SerializeField]
    private bool randomLocations = false;

    // Spawn cars on start
    void Start()
    {
        Invoke("RespawnCar",1);
    }

    // Respawn cars during runtime
    public void RespawnCar()
    {
        var road = RoadManager.Instance.GetRoads();
        foreach (Car car in cars)
        {
            Destroy(car.gameObject);
        }
        cars.Clear();
        for (int i = 0; i < spawnCount; i++)
        {
            Car car = Instantiate<Car>(Resources.Load<Car>("Prefabs/Cars/Car"));
            cars.Add(car);
            car.SetSpeed(Random.Range(0.1f,0.5f));
            if (randomLocations)
            {
                car.transform.position = road[(Random.Range(0, road.Count - 1))].transform.position;
            }
        }
    }
}
