/*
 * This code is part of Jerome Chetty Summision for FuzzyLogic first stage 2021
 */
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

    //used to manager all start related objects
    public delegate void OnStart();
    public static OnStart onStart;

    private void Start()
    {
        Invoke("InvokedStart",1);
    }

    private void InvokedStart()
    {
        onStart.Invoke();
    }

}
