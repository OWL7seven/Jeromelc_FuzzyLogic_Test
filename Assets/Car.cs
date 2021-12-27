using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private BoxCollider collider;
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private Road currentRoad;
    [SerializeField]
    private Road passRoad;
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private Vector3 direction = new Vector3(0, 0, 1);
    [SerializeField]
    private Vector3 directionOffset = new Vector3(0, 0.2f, 0);
    [SerializeField]
    private float distance = 10;
    [SerializeField]
    private Vector3 rayDirection = new Vector3(0, -0.1f, 1);
    [SerializeField]
    private Vector3 rayOffset = new Vector3(0, 0, 0);

    private void Start()
    {
        collider = this.GetComponent<BoxCollider>();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + rayOffset, transform.TransformDirection(rayDirection), out hit, distance, layerMask))
        {
            Debug.DrawRay(transform.position + rayOffset, transform.TransformDirection(rayDirection) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            if (hit.collider.gameObject.GetComponent<Road>() != null)
            {
                if (currentRoad != hit.collider.gameObject.GetComponent<Road>())
                {
                    currentRoad = hit.collider.gameObject.GetComponent<Road>();
                    if (currentRoad.gameObject.tag == "TJunction")
                    {
                       // transform.position = currentRoad.transform.position + directionOffset;
                        transform.Rotate(0, 90, 0);
                        currentRoad = null;
                        if (direction == Vector3.right)
                        {
                            direction = Vector3.back;
                        }
                        else if (direction == Vector3.back)
                        {
                            direction = Vector3.left;
                        }
                        else if (direction == Vector3.left)
                        {
                            direction = Vector3.forward;
                        }
                        else
                        {
                            direction = Vector3.right;
                        }
                        passRoad = currentRoad;
                    }
                    else if (currentRoad.gameObject.tag == "Fourway")
                    {
                        direction = RandomDirection(direction);
                        passRoad = currentRoad;
                    }
                }
            }
            else
            {
                Debug.DrawRay(transform.position + rayOffset, transform.TransformDirection(rayDirection) * 1000, Color.white);
                //Debug.Log("Did not Hit");

            }
        }
        transform.position += direction * speed;
    }

    private Vector3 RandomDirection(Vector3 currentDirection)
    {
        int random = Random.Range(0, 3);

        //left
        if (random == 0)
        {
            if (currentDirection != Vector3.left)
            {
                transform.Rotate(0, Vector3.Angle(currentDirection, Vector3.left), 0);
                return Vector3.left;
            }
            else
            {
                return currentDirection;
            }
        }
        //Forward
        else if (random == 1)
        {
            if (currentDirection != Vector3.forward)
            {
                transform.Rotate(0, Vector3.Angle(currentDirection, Vector3.forward), 0);
                return Vector3.forward;
            }
            else
            {
                return currentDirection;
            }
        }
        //right
        else if (random == 2)
        {
            if (currentDirection != Vector3.right)
            {
                transform.Rotate(0, Vector3.Angle(currentDirection, Vector3.right), 0);
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
            if (currentDirection != Vector3.back)
            {
                transform.Rotate(0, Vector3.Angle(currentDirection, Vector3.back), 0);
                return Vector3.back;
            }
            else
            {
                return currentDirection;
            }
        }

        return currentDirection;
    }

    /*
    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        currentRoad =  collision.gameObject.GetComponent<Road>();
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        currentRoad = null;
    }
    */
}
