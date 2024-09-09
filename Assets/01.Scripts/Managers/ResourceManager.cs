using System;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public event Action<int> OnResourceChnaged;
    public int ResourceAmount { get; private set; } = 1000;

    public void AddResource(int amount)
    {
        ResourceAmount += amount;
        OnResourceChnaged?.Invoke(ResourceAmount);

    }
    
    public void UseResource(int amount)
    {
        ResourceAmount -= amount;
        OnResourceChnaged?.Invoke(ResourceAmount);
    }
}