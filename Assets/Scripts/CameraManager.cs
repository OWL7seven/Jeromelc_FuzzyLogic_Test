using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> locations;
    [SerializeField]
    private List<Vector3> rotations;
    [SerializeField]
    private int timer = 5;
    [SerializeField]
    private int timerCounter;

    void Start()
    {
        Vector3 position;
        Vector3 rotation;

        //main
        position = new Vector3(-5,50,-45);
        locations.Add(position);
        position = new Vector3(65, 18, 63);
        locations.Add(position);
        position = new Vector3(209, 18, 31);
        locations.Add(position);
        position = new Vector3(140, 18, -24);
        locations.Add(position);

        rotation = new Vector3(45, 45, 0);
        rotations.Add(rotation);
        rotation = new Vector3(45, 45, 0);
        rotations.Add(rotation);
        rotation = new Vector3(45, -63, 0);
        rotations.Add(rotation);
        rotation = new Vector3(45, 0, 0);
        rotations.Add(rotation);

        InvokeRepeating("Timer", 0,1);
    }

    // Update is called once per frame
    private void Timer()
    {
        if (timerCounter > 0)
        {
            timerCounter--;
        }
        else
        {
            int value = Random.Range(0, locations.Count);
            Camera.main.transform.position = locations[value];
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(rotations[value]), 1);
            timerCounter = timer;
        }
    }

    public void CycleCamera()
    {
        int value = Random.Range(0, locations.Count);
        Camera.main.transform.position = locations[value];
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(rotations[value]), 1);
        timerCounter = timer;
    }
}
