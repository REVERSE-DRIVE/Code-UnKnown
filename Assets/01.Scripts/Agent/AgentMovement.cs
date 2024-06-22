using System;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    protected Agent _owner;

    protected void Awake()
    {
        _owner = GetComponent<Agent>();
    }
}