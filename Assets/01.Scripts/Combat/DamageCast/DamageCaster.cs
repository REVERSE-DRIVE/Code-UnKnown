using UnityEngine;

public abstract class DamageCaster : MonoBehaviour
{
    [SerializeField] protected LayerMask _whatIsAgent;
    [SerializeField] protected int _damage = 10;

    protected Agent _owner;
    public void Init(Agent owner)
    {
        _owner = owner;
        _damage = owner.Stat.GetDamage();
    }
    
    public abstract void CastDamage();
}