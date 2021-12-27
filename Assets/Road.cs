using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    private Vector2 location;

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
}
