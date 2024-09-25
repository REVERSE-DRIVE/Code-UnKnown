using System;
using UnityEngine;

public class RayDamageCaster : DamageCaster
{
    public float _distance = 3f;
    public event Action OnCastEvent;

    public Vector2 Direction
    {
        get => transform.right;
        set => transform.right = value;
    }
    
    public void Init(Agent owner, Vector2 direction)
    {
        base.Init(owner);
        Direction = direction;
    }
    public override bool CastDamage(int damage, float radius = 1)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, _distance, _whatIsAgent);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Agent agent))
            {
                agent.HealthCompo.TakeDamage(damage);
                OnCastEvent?.Invoke();
                return true;
            }
        }
        else
        {
            return false;
        }
        
        return false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)Direction * _distance);
        Gizmos.color = Color.white;
    }
}