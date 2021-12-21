using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    //Road block types
    public enum RoadType
    {
        Straight,
        Corner,
        Junction,
        tJunction
        // add stops, robots, etc
    }

    //Vehicle types
    public enum VechicleType
    {
        Car,
        Van,
        Truck
    }
    //Traffic robot states
    public enum RobotState
    {
        Green,
        Yellow,
        Red
    }

    //Vehicle states
    public enum VehicleState
    {
        Stop,
        Moving,
        Queuing
    }
}
