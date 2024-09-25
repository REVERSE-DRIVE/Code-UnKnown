using System;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public event Action<int> OnResourceChanged;
    public int ResourceAmount;

    public void AddResource(int amount)
    {
        ResourceAmount += amount;
        OnResourceChanged?.Invoke(ResourceAmount);

    }
    
    public void UseResource(int amount)
    {
        ResourceAmount -= amount;
        Debug.Log("ResourceAmount: " + ResourceAmount);
        OnResourceChanged?.Invoke(ResourceAmount);
    }
}