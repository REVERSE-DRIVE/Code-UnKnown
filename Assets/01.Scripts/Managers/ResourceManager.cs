using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public int ResourceAmount { get; private set; } = 1000;

    public void AddResource(int amount)
    {
        ResourceAmount += amount;
    }
    
    public void UseResource(int amount)
    {
        ResourceAmount -= amount;
    }
}