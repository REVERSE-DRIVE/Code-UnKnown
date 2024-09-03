using System;
using UnityEngine;

public class InGameCounter : MonoSingleton<InGameCounter>
{
    public float TimeCounter { get; private set; }
    public int PartCounter { get; private set; }
    private void Update()
    {
        TimeCounter += Time.deltaTime;
    }
}