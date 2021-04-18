using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }
    [SerializeField] SoundManager soundManager;
    
    private void Awake()
    {
        AssignSingletonInstance();
    }
    
    private void AssignSingletonInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("More than one AppManager exist", this);
    }
    
    
}
