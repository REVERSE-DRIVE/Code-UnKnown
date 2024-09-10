using ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

public class DummyObject : MonoBehaviour, IDamageable, IPoolable
{
    public UnityEvent OnHitEvent;
    [field:SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;
    
    public void TakeDamage(int amount)
    {
        OnHitEvent?.Invoke();
    }

    public void CheckDie()
    {
        
    }

    
    public void ResetItem()
    {
    }
}