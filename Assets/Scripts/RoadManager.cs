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

    [SerializeField]
    private List<Road> roads = new List<Road>();

    private int currentBlockX;
    private int currentBlockY;

    public List<Road> GetRoads()
    {
        return roads;
    }

    public void RemoveRoad(Road road)
    {
        roads.Remove(road);
    }

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
            currentBlockX = i;
            currentBlockY = 0;
            AddRoadBlock(new Vector3(i * ((blocksWidth - 1) * 10), 0, 0));
            //equals 1 so because X creates the first y block
            for (int k = 1; k < blocksY; k++)
            {
                currentBlockY = k;
                AddRoadBlock(new Vector3(i * ((blocksWidth - 1) * 10), 0, k * ((blocksHeight - 1) * 10)));
            }
        }
    }

    private void AddRoadBlock(Vector3 position)
    {
        GameObject block = new GameObject();

        //Left section
        for (int k = 0; k < blocksWidth - 2; k++)
        {
            Road road = null;
            //if first block
            if (k == 0)
            {
                road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/Straight"), block.transform);
            }
            //if last block
            else if (k == blocksHeight - 1)
            {
                road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/Straight"), block.transform);
            }
            else
            {
                road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/Straight"), block.transform);
            }
            if (road != null)
            {
                roads.Add(road);
                road.transform.localPosition = new Vector3(blockSize + k * blockSize, 0, (blocksHeight - 1) * -blockSize);
            }
        }

        //Bottom section - mostly straights going from top to bottom, and left side
        for (int k = 0; k < blocksHeight - 1; k++)
        {
            Road road = null;
            //if first block
            if (k == 0)
            {
                road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/Straight"), block.transform);
                road.transform.localPosition = new Vector3(0, 0, -(k * blockSize + 10));
                road.transform.Rotate(new Vector3(0, 90, 0));
            }
            //if last block
            else if (k == blocksHeight - 2)
            {
                if (currentBlockX == 0)
                {
                    //corner
                    if (currentBlockY == 0)
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/corner"), block.transform);
                        road.transform.localPosition = new Vector3(0, 0, -(k * blockSize + 10));
                        road.transform.Rotate(new Vector3(0, 0, 0));
                    }
                    else
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/tjunction"), block.transform);
                        road.transform.localPosition = new Vector3(0, 0, -(k * blockSize + 10));
                        road.transform.Rotate(new Vector3(0, 90, 0));
                    }
                }
                else 
                {
                    if (currentBlockY == 0)
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/tjunction"), block.transform);
                        road.transform.localPosition = new Vector3(0, 0, -(k * blockSize + 10));
                        road.transform.Rotate(new Vector3(0, 0, 0));
                    }
                    else
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/fourway"), block.transform);
                        road.transform.localPosition = new Vector3(0, 0, -(k * blockSize + 10));
                        road.transform.Rotate(new Vector3(0, 90, 0));
                    }
                }

            }
            else 
            {
                road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/Straight"), block.transform);
                road.transform.localPosition = new Vector3(0, 0, -(k * blockSize + 10));
                road.transform.Rotate(new Vector3(0, 90, 0));
            }
            if (road != null)
            {
                roads.Add(road);
            }
        }
        //Top section
        if (currentBlockY == blocksY-1)
        {            
            for (int k = 0; k < blocksWidth - 1; k++)
            {
                Road road = null;

                //if first block
                if (k == 0)
                {
                    if (currentBlockX == 0)
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/corner"), block.transform);
                        road.transform.localPosition = new Vector3(k * blockSize, 0, 0);
                        road.transform.Rotate(new Vector3(0, 90, 0));
                    }
                    else
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/tjunction"), block.transform);
                        road.transform.localPosition = new Vector3(k * blockSize, 0, 0);
                        road.transform.Rotate(new Vector3(0, 180, 0));
                    }

                }
                //if last block
                else if (k == blocksWidth - 1)
                {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/corner"), block.transform);
                        road.transform.localPosition = new Vector3(k * blockSize , 0, 0);
                        road.transform.Rotate(new Vector3(0, 180, 0));
                }
                else
                {
                    road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/Straight"), block.transform);
                    road.transform.localPosition = new Vector3(k * blockSize, 0, 0);
                }
                if (road != null)
                {                    
                    roads.Add(road);                   
                }
            }
        }
        //Right section
        if (currentBlockX == blocksX-1)
        {       
            for (int k = 0; k < blocksHeight; k++)
            {
                Road road = null;

                //if first block
                if (k == 0)
                {
                    if (currentBlockY == 0)
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/tjunction"), block.transform);
                        road.transform.localPosition = new Vector3((blocksWidth - 1) * blockSize, 0, -(k * blockSize + 10) + blockSize);
                        road.transform.Rotate(new Vector3(0, 270, 0));
                        Debug.Log(road.transform.position);
                    }
                    else if (currentBlockY == blocksY - 1)
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/corner"), block.transform);
                        road.transform.localPosition = new Vector3((blocksWidth - 1) * blockSize, 0, -(k * blockSize+10) + blockSize);
                        road.transform.Rotate(new Vector3(0, 180, 0));
                        Debug.Log(road.transform.position);
                    }
                    else
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/tjunction"), block.transform);
                        road.transform.localPosition = new Vector3((blocksWidth - 1) * blockSize, 0, -(k * blockSize + 10) + blockSize);
                        road.transform.Rotate(new Vector3(0, 270, 0));

                    }
                }
                //if last block
                else if (k == blocksHeight - 1)
                {
                    if (currentBlockY == 0)
                    {
                        road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/corner"), block.transform);
                        road.transform.localPosition = new Vector3((blocksWidth - 1) * blockSize, 0, -(k * blockSize + 10) + blockSize);
                        road.transform.Rotate(new Vector3(0, 270, 0));
                    }
                    else 
                    {                       
                        continue;
                    }
                }
                else
                {
                    road = Instantiate<Road>(Resources.Load<Road>("Prefabs/Roads/Updated/Straight"), block.transform);
                    road.transform.localPosition = new Vector3((blocksWidth - 1) * blockSize, 0, -(k * blockSize + 10) + blockSize);
                    road.transform.Rotate(new Vector3(0, 90, 0));
                }
                if (road != null)
                {
                    roads.Add(road);
                }
            }
        }

        block.transform.position = position;
        var blockRoads = block.GetComponentsInChildren<Road>();
        foreach (Road road in blockRoads)
        {
            road.transform.parent = null;
            road.name = $"({road.transform.position.x},{road.transform.position.z})";
           // Debug.Log(road.transform.position);
        }
        Destroy(block.gameObject);
    }

    private void RemoveRoads()
    {
        foreach (Road road in roads)
        {
            DestroyImmediate(road.gameObject);
        }
        roads.Clear();
    }

    public void Rebuild()
    {
        RemoveRoads();
        Build();
    }
}
