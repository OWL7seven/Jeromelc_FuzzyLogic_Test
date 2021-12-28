using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    private Vector2 location;
    [SerializeField]
    private List<Car> cars = new List<Car>();
    [SerializeField]
    private int timer = 2;
    [SerializeField]
    private int timerCounter;

    public Vector2 GetLocation()
    {
        return location;
    }

    [SerializeField]
    private MeshCollider Collider;

    public MeshCollider GetCollider()
    {
        return Collider;
    }

    public void AddCar(Car car)
    {
        if (gameObject.tag == "Fourway")
        {
            cars.Add(car);
            car.Stop(true);

            if (!IsInvoking("AllowCarToProgress"))
            {
                InvokeRepeating("AllowCarToProgress", 0, 1);
            }
        }
        if (gameObject.tag == "TJunction")
        {
            cars.Add(car);
            car.Stop(true);

            if (!IsInvoking("AllowCarToProgress"))
            {
                InvokeRepeating("AllowCarToProgress", 0, 1);
            }
        }
        if (gameObject.tag == "Zebra")
        {
            cars.Add(car);
            car.Stop(true);

            if (!IsInvoking("AllowCarToProgress"))
            {
                InvokeRepeating("AllowCarToProgress", 0, 1);
            }
        }
    }

    //Timer system that gives permission to cars in order to drive
    //this can be used for zebra crossing as well.

    private void AllowCarToProgress()
    {
        if (cars.Count > 0)
        {
            if (timerCounter > 0)
            {
                timerCounter--;
            }
            else
            {
                cars[0].Stop(false);
                cars.Remove(cars[0]);
                timerCounter = timer;
            }
        }
    }
}
