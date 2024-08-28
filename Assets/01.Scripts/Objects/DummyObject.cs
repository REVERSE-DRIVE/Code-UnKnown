using ObjectPooling;
using UnityEngine;

public class DummyObject : MonoBehaviour, IDamageable, IPoolable
{
    [field:SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;
    
    public void TakeDamage(int amount)
    {
    }

    public void CheckDie()
    {
        
    }

    
    public void ResetItem()
    {
    }
}