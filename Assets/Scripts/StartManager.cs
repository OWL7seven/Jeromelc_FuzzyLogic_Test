using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public static StartManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }
}
