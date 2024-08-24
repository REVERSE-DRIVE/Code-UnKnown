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
        _owner.DamageCasterCompo.CastDamage(3f, _damage);
    } 
}