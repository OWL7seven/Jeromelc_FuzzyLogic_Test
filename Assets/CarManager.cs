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

    [SerializeField]
    private List<Car> cars = new List<Car>();
    [SerializeField]
    private bool randomLocations = false;

    [SerializeField]
    private int carCount = 100;
    [SerializeField]
    private int busCount = 10;
    [SerializeField]
    private int truckCount = 10;
    [SerializeField]

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
        for (int i = 0; i < carCount; i++)
        {
            Car car = Instantiate<Car>(Resources.Load<Car>("Prefabs/Cars/Car"));
            cars.Add(car);
            car.SetSpeed(Random.Range(0.1f,0.5f));
            if (randomLocations)
            {
                AddRandom(road,car);
            }
        }
        for (int i = 0; i < busCount; i++)
        {
            Car car = Instantiate<Car>(Resources.Load<Car>("Prefabs/Cars/Bus"));
            cars.Add(car);
            car.SetSpeed(Random.Range(0.125f, 0.2f));
            if (randomLocations)
            {
                AddRandom(road, car);
            }
        }
        for (int i = 0; i < truckCount; i++)
        {
            Car car = Instantiate<Car>(Resources.Load<Car>("Prefabs/Cars/Truck"));
            cars.Add(car);
            car.SetSpeed(Random.Range(0.05f, 0.1f));
            if (randomLocations)
            {
                AddRandom(road, car);
            }
        }
    }

    private void AddRandom(List<Road> road,Car car)
    {
        Road currentRoad = road[(Random.Range(0, road.Count - 1))];
        car.SetRoad(currentRoad);
        if (currentRoad.gameObject.tag == "Fourway")
        {
            AddRandom(road, car);
        }
        else if (currentRoad.gameObject.tag == "TJunction")
        {
            AddRandom(road, car);
        }
        else if (currentRoad.gameObject.tag == "Corner")
        {
            AddRandom(road, car);
        }
        else if (currentRoad.gameObject.tag == "Junction")
        {
            AddRandom(road, car);
        }
        else 
        {
            car.transform.position = currentRoad.transform.position;
            car.SetPosition();
        }       
    }
}
