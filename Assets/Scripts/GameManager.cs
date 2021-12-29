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
    public delegate void OnFixedUpdate();

    public static OnFixedUpdate onFixedUpdate;

    public delegate void OnUpdate();

    public static OnUpdate onUpdate;

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
