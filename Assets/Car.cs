using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private MeshCollider collider;
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private Road currentRoad;
    [SerializeField]
    private Road passRoad;
    [SerializeField]
    private Car currentCar;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float turnSpeed = 50;
    [SerializeField]
    private Vector3 direction = new Vector3(0, 0, 1);
    [SerializeField]
    private Vector3 directionOffset;
    [SerializeField]
    private float distance = 10;
    [SerializeField]
    private Vector3 rayDirection = new Vector3(0, -0.1f, 1);
    [SerializeField]
    private Vector3 rayOffset = new Vector3(0, 0, 0);
    [SerializeField]
    private float turnDistance = 1;
    [SerializeField]
    private float currentTurnDistance;
    [SerializeField]
    private float followingDistance = 1;
    [SerializeField]
    private Vector3 carRayOffset = new Vector3(0, 1, 0);
    [SerializeField]
    private bool debug;
    [SerializeField]
    private bool stop;

    private void Awake()
    {
        collider = this.GetComponent<MeshCollider>();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    public void Stop(bool value)
    {
        stop = value;
    }

    private void FixedUpdate()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayOffset, transform.TransformDirection(rayDirection), out hit, distance, layerMask))
        {
            if (debug)
            {
                Debug.DrawRay(transform.position + rayOffset, transform.TransformDirection(rayDirection) * hit.distance, Color.yellow);
            }
            if (hit.collider.gameObject.GetComponent<Road>() != null)
            {
                if (currentRoad != hit.collider.gameObject.GetComponent<Road>())
                {
                    currentRoad = hit.collider.gameObject.GetComponent<Road>();
                    if (currentRoad != passRoad)
                    { 
                        currentRoad.AddCar(this);
                    }
                }
            }
        }
        if (!stop)
        {
            if (currentRoad != null)
            {
                currentTurnDistance = Vector3.Distance(currentRoad.transform.position, transform.position);
                if (turnDistance > currentTurnDistance)
                {
                    if (currentRoad != passRoad)
                    {
                        //Used to control what the car does at each junction
                        //A offset is added to keep the car on the left lane
                        //A bool could be added to swap between left and right lanes
                        if (currentRoad.gameObject.tag == "TJunction")
                        {
                            // the road is facing towards Z
                            // if the turning is facing the -Z axis of the road then 

                            // so if you going in a left direction you allowed to continue or go forward direction.
                            // if you going backwards compared to the Tjunction you can turn left of right
                            // if going right then you can go forward or turn 

                            // can only turn right or go straight
                            //coming from the right of the tjunction
                            if (direction == -currentRoad.transform.right)
                            {
                                if (currentRoad.transform.right == Vector3.forward)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, -1);
                                }
                                //bottom x row
                                else if (currentRoad.transform.right == Vector3.right)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(-1, 0, 0);
                                }
                                //Left Z row
                                else if (currentRoad.transform.right == Vector3.back)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, 1);
                                }
                                //top x row
                                else if (currentRoad.transform.right == Vector3.left)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(1, 0, 0);
                                }
                                direction = currentRoad.transform.forward;
                            }
                            // can turn left and right
                            else if (direction == -currentRoad.transform.forward)
                            {
                                //checking to see if the right of the road is facing in the direction of the global direction
                                //going down x to turn left
                                //right z row
                                if (currentRoad.transform.right == Vector3.forward)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(-1, 0, 0);
                                }
                                //bottom x row
                                else if (currentRoad.transform.right == Vector3.right)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, 1);
                                }
                                //Left Z row
                                else if (currentRoad.transform.right == Vector3.back)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(1, 0, 0);
                                }
                                //top x row
                                else if (currentRoad.transform.right == Vector3.left)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, -1);
                                }
                                //transform.position = currentRoad.transform.position + new Vector3(1,0,0);
                                direction = currentRoad.transform.right;
                            }
                            //coming from the left of the T junction
                            else if (direction == currentRoad.transform.right)
                            {
                                if (currentRoad.transform.right == Vector3.forward)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, -1);
                                }
                                //bottom x row
                                else if (currentRoad.transform.right == Vector3.right)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(-1, 0, 0);
                                }
                                //Left Z row
                                else if (currentRoad.transform.right == Vector3.back)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, 1);
                                }
                                //top x row
                                else if (currentRoad.transform.right == Vector3.left)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(1, 0, 0);
                                }
                                direction = currentRoad.transform.forward;
                            }
                            passRoad = currentRoad;
                        }
                        else if (currentRoad.gameObject.tag == "Fourway")
                        { 
                            direction = RandomDirection(direction);
                            passRoad = currentRoad;
                        }
                        else if (currentRoad.gameObject.tag == "Corner")
                        {
                            // can only right
                            if (direction == -currentRoad.transform.right)
                            {
                                if (currentRoad.transform.right == Vector3.forward)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, -1);
                                }
                                //bottom x row
                                else if (currentRoad.transform.right == Vector3.right)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(-1, 0, 0);
                                }
                                //Left Z row
                                else if (currentRoad.transform.right == Vector3.back)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, 1);
                                }
                                //top x row
                                else if (currentRoad.transform.right == Vector3.left)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(1, 0, 0);
                                }
                                direction = currentRoad.transform.forward;
                            }
                            // can turn left
                            else if (direction == -currentRoad.transform.forward)
                            {
                                if (currentRoad.transform.right == Vector3.forward)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, -1);
                                }
                                //bottom x row
                                else if (currentRoad.transform.right == Vector3.right)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(-1, 0, 0);
                                }
                                //Left Z row
                                else if (currentRoad.transform.right == Vector3.back)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(0, 0, 1);
                                }
                                //top x row
                                else if (currentRoad.transform.right == Vector3.left)
                                {
                                    transform.position = currentRoad.transform.position + new Vector3(1, 0, 0);
                                }
                                direction = currentRoad.transform.right;
                            }                    
                            passRoad = currentRoad;
                        }                       
                    }
                }
            }
        }
        // checking if car is infront to slow down or stop
        RaycastHit carHit;
        if (Physics.Raycast(transform.position + carRayOffset, transform.forward, out carHit, followingDistance, layerMask))
        {
            if (debug)
            {
                Debug.DrawRay(transform.position + carRayOffset, transform.forward * carHit.distance, Color.red);
            }
            if (carHit.collider.gameObject.GetComponent<Car>() != null)
            {
                if (currentCar != carHit.collider.gameObject.GetComponent<Car>())
                {
                    currentCar = carHit.collider.gameObject.GetComponent<Car>();
                }
            }
        }
        else
        {
            if (debug)
            {
                Debug.DrawRay(transform.position + carRayOffset, transform.forward * carHit.distance, Color.green);
            }
            currentCar = null;
        }
        if (!stop)
        {
            if (currentCar != null)
            {
                if (followingDistance > Vector3.Distance(currentCar.transform.position, this.transform.position))

                {
                    transform.position += direction * speed;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed);
                }
            }
            else
            {
                transform.position += direction * speed;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed);
            }
        }
    }


    private Vector3 RandomDirection(Vector3 currentDirection)
    {
        //checks if car is going in a certain direction
        // if the car is going in that direction it can choose to go straight or turn into the allowed directions

        int random = Random.Range(0, 4);

        //left
        if (random == 0)
        {
            if (currentDirection != Vector3.left && currentDirection != Vector3.right)
            {
                transform.position = currentRoad.transform.position + new Vector3(0, 0, -1);
                return Vector3.left;
            }
            else
            {
                return currentDirection;
            }
        }
        //Forward - z axis - blue
        else if (random == 1)
        {
            if (currentDirection != Vector3.forward && currentDirection != Vector3.back)
            {
                transform.position = currentRoad.transform.position + new Vector3(-1, 0, 0);
                return Vector3.forward;
            }
            else
            {
                return currentDirection;
            }
        }
        //right - x axis - red
        else if (random == 2)
        {
            if (currentDirection != Vector3.right && currentDirection != Vector3.left)
            {
                transform.position = currentRoad.transform.position + new Vector3(0, 0, 1);
                return Vector3.right;
            }
            else
            {
                return currentDirection;
            }
        }
        //back
        else if (random == 3)
        {
            if (currentDirection != Vector3.back && currentDirection != Vector3.forward)
            {
                transform.position = currentRoad.transform.position + new Vector3(1, 0, 0);
                return Vector3.back;
            }
            else
            {
                return currentDirection;
            }
        }

        return currentDirection;
    }

    public void SetPosition()
    {
         if (currentRoad.gameObject.tag == "Straight")
        {
            // can only right
            if (direction == -currentRoad.transform.forward)
            {
                transform.position = currentRoad.transform.position + new Vector3(0, 0, -1);
                direction = -currentRoad.transform.right;
            }
            // can turn left
            else if (direction == currentRoad.transform.forward)
            {
                transform.position = currentRoad.transform.position + new Vector3(0, 0, 1);
                direction = currentRoad.transform.right;
            }
            // can turn left
            else if (direction == currentRoad.transform.right)
            {
                transform.position = currentRoad.transform.position + new Vector3(1, 0, 0);
                direction = currentRoad.transform.right;
            }
            // can turn left
            else if (direction == -currentRoad.transform.right)
            {
                transform.position = currentRoad.transform.position + new Vector3(-1, 0, 0);
                direction = -currentRoad.transform.right;
            }
            passRoad = currentRoad;
        }
    }

    public void SetRoad(Road road)
    {
        currentRoad = road;  
    }
}
