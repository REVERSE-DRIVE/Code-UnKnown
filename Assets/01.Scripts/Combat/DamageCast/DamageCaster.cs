using UnityEngine;

public abstract class DamageCaster : MonoBehaviour
{
    [SerializeField] protected LayerMask _whatIsAgent;
    
    public abstract void CastDamage();
}