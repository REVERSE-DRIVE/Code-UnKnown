using ObjectPooling;
using UnityEngine;

public class ImmediateBomb : ExplodeObject, IPoolable
{
    [field:SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;
    
    private void OnEnable()
    {
        Explode();
    }

    public void ResetItem()
    {
        
    }
}
