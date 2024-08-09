using UnityEngine;

public abstract class DamageCaster : MonoBehaviour
{
    [SerializeField] protected LayerMask _whatIsAgent;
    [SerializeField] protected int _damage = 10;
    
    public abstract void CastDamage();
}