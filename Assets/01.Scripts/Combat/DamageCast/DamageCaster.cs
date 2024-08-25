using UnityEngine;

public abstract class DamageCaster : MonoBehaviour
{
    [SerializeField] protected LayerMask _whatIsAgent;

    protected Agent _owner;
    public void Init(Agent owner)
    {
        _owner = owner;
    }
    
    public abstract bool CastDamage(float radius, int damage);
}