using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    private MeshCollider meshCollider;

    public void Start()
    {
        meshCollider = this.GetComponentInChildren<MeshCollider>();
    }
}
