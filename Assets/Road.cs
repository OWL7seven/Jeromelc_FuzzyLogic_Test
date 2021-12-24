using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    private BoxCollider Collider;

    public BoxCollider GetCollider()
    {
        return Collider;
    }
}
