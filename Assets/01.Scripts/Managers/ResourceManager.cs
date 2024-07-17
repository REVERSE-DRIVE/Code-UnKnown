using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int ResourceAmount { get; private set; }

    public void AddResource(int amount)
    {
        ResourceAmount += amount;
    }
    
    public void UseResource(int amount)
    {
        ResourceAmount -= amount;
    }
}