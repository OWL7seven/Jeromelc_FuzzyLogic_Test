using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Car car;

    void Start()
    {
        car = Instantiate<Car>(Resources.Load<Car>("Prefabs/Cars/Car"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnCar()
    {
        Destroy(car.gameObject);
        car = Instantiate<Car>(Resources.Load<Car>("Prefabs/Cars/Car"));
    }
}
