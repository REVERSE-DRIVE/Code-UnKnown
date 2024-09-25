using System;
using UnityEngine;

public class DecoySword : MonoBehaviour
{
    [SerializeField]private DecoyEnemy _owner;
    
    private int _damage;

    private void Start()
    {
        _damage = _owner.Stat.GetDamage();
    }

    public void Attack()
    {
        bool success = _owner.DamageCasterCompo.CastDamage(_damage, 3f);
        if (success)
        {
            CameraManager.Instance.Shake(5f, 0.1f);
        }
    } 
}