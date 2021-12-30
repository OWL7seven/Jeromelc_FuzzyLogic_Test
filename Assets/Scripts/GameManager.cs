/*
 * This code is part of Jerome Chetty Summision for FuzzyLogic first stage 2021
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;  
    }
    //used to manager all fixed update related objects
    public delegate void OnFixedUpdate();
    public static OnFixedUpdate onFixedUpdate;

    //used to manager all normal update related objects
    public delegate void OnUpdate();
    public static OnUpdate onUpdate;

    //used to manager all invoked by 1 second related objects
    public delegate void OnInvokedUpdate();
    public static OnInvokedUpdate onInvokedUpdate;

    private void Update()
    {
        onUpdate?.Invoke();
    }
    private void FixedUpdate()
    {
        onFixedUpdate?.Invoke();
    }

    private void UpdateOncePerFrame()
    {
        onInvokedUpdate?.Invoke();
    }
    private void OnEnable()
    {
        InvokeRepeating("UpdateOncePerFrame", 0, 1);
    }
}
