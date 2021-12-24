using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    //Add to start manager
    //startManager.instance.OnStart += Start();

    private void Start()
    {
        Build();
    }


    public int blocksX = 1;
    public int blocksY = 1;

    public int blocksWidth = 5;
    public int blocksHeight = 5;

    public int blockSize = 10;

    private List<Road> roads = new List<Road>();
    private List<GameObject> blocks = new List<GameObject>();

    //Testing
    private void Build()
    {
        //Build 2 block like example
        //each block is 4 blocks wide.
        //1 corner (dynamic) - checks at runtime what road block to be
        //1 junction (dynamic)
        // 2 straights

        //if dynamic then blocks are just placed down. making generating something easier
        // int number of blocks wide
        // int number of blocks high

        //depending on above the corners will dynamically change to the prefab that fits.
        //will need to create a system that will add zebra crossing, junctions, and t junctions
        //must have 2 or more blocks to work

        for (int i = 0; i < blocksX; i++)
        {
            AddRoadBlock(new Vector3(i * ((blocksWidth-1)*10), 0, 0));
            //equals 1 so because X creates the first y block
            for (int k = 1; k < blocksY; k++)
            {
                AddRoadBlock(new Vector3(i * ((blocksWidth - 1) * 10), 0, k * ((blocksHeight - 1) * 10)));
            }
        }
    }

    private void AddRoadBlock(Vector3 position)
    {
        //Create block
        //Move block to blockX value 
        //for y do x

        //Create one perfect block
        GameObject block = new GameObject();

        //Bottom section
        for (int k = 0; k < blocksWidth - 1; k++)
        {
            Road road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Road_Straight_Prefab"), block.transform);
            road.transform.localPosition = new Vector3(blockSize + k * blockSize, 0, (blocksHeight - 1) * -blockSize);

            //if first block
            if (k == 0)
            {
               
            }

            //if last block
            if (k == blocksHeight - 1)
            {

            }

            roads.Add(road);           
        }

        //Left section
        for (int k = 0; k < blocksHeight - 1; k++)
        {
            Road road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Road_Straight_Prefab"), block.transform);
            road.transform.localPosition = new Vector3(0, 0, -(k * blockSize));
            road.transform.Rotate(new Vector3(0, 90, 0));
            roads.Add(road);

            //if first block
            if (k == 0)
            {

            }

            //if last block
            if (k == blocksHeight - 1)
            {

            }
        }
      
        //Top section
        for (int k = 0; k < blocksWidth - 1; k++)
        {
            Road road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Road_Straight_Prefab"), block.transform);
            road.transform.localPosition = new Vector3(k * blockSize, 0, 0);
            roads.Add(road);

        }
        //Right section
        for (int k = 0; k < blocksHeight - 1; k++)
        {
            Road road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Road_Straight_Prefab"), block.transform);
            road.transform.localPosition = new Vector3((blocksWidth - 1) * blockSize, 0, -(k * blockSize) + blockSize);
            road.transform.Rotate(new Vector3(0, 90, 0));
            roads.Add(road);
        }
   
       
      

        block.transform.position = position;
        block.name = $"Block({position.x},{position.z})";
        blocks.Add(block);
    }

    private void RemoveRoads()
    {
        foreach (Road road in roads)
        {
            DestroyImmediate(road.gameObject);
        }
        roads.Clear();

        foreach (GameObject block in blocks)
        {
            DestroyImmediate(block.gameObject);
        }
        blocks.Clear();
    }

    public void Rebuild()
    {
        RemoveRoads();
        Build();
    }
}
