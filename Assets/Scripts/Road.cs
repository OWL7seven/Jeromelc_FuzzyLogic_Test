/*
 * This code is part of Jerome Chetty Summision for FuzzyLogic first stage 2021
 */
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
    private int timer = 3;
    [SerializeField]
    private int timerCounter;
    [SerializeField]
    private List<BoxCollider> walkColliders;


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
    [SerializeField]
    private List<Pedestrian> peds = new List<Pedestrian>();

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
        if (gameObject.tag == "Crossing")
        {
            cars.Add(car);
            if (peds.Count > 0)
            {
                car.Stop(true);
            }

            if (!IsInvoking("AllowCarToProgressAfterPedestrian"))
            {
                InvokeRepeating("AllowCarToProgressAfterPedestrian", 0, 1);
            }

        }
    }

    //Timer system that gives permission to cars in order to drive
    //this can be used for zebra crossing as well.

    private void AllowCarToProgress()
    {
        if (peds.Count > 0)
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
                    if (cars.Count > 2)
                    {
                        timerCounter = timer * 2;
                    }
                    else
                    {
                        timerCounter = timer;
                    };
                }
            }
        }
        else
        {
            if (cars.Count > 0)
            {
                cars[0].Stop(false);
                cars.Remove(cars[0]);
            }
        }
    }

    private void AllowCarToProgressAfterPedestrian()
    {
        if (peds.Count == 0)
        {
            if (cars.Count > 0)
            {
                cars[0].Stop(false);
                cars.Remove(cars[0]);
            }
        }
    }

    // Adds or removes a pedestrain from the list which the car uses to decide whether it can move or not
    public void AddPed(Pedestrian ped, bool add)
    {
        if (add)
        {
            peds.Add(ped);
        }
        else
        {
            peds.Remove(ped);
        }
    }
    void Start()
    {
        Invoke("RemoveCollider", 1);
    }

    //removes colliders which are used to create the navmesh at the start of runtime
    private void RemoveCollider()
    {
        foreach (var obj in GetComponentsInChildren<BoxCollider>())
        {
            Destroy(obj.gameObject);
        }
        if (gameObject.tag == "Crossing")
        {
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(10, 0.1f, 5);
        }
    }
}
