using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialBackDoor : MonoBehaviour
{
    public UnityEvent OnEnterEvent;
    private BackDoorVisual _visual;
    
    private void Awake()
    {
        _visual = GetComponentInChildren<BackDoorVisual>();
        OnEnterEvent.AddListener(HandleSetActiveVisual);
    
    }

    private void HandleSetActiveVisual()
    {
        _visual.Active();
        
    }

    private void OnTriggerEnter2D()
    {
        OnEnterEvent?.Invoke();

    }   
}
