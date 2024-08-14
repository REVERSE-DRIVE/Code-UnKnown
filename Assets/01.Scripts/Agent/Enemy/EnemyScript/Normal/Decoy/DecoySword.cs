using System;
using UnityEngine;

public class DecoySword : MonoBehaviour
{
    private DecoyEnemy _owner;

    private void Awake()
    {
        _owner = transform.root.GetComponent<DecoyEnemy>();
    }

    public void Attack()
    {
        _owner.DamageCasterCompo.CastDamage(5f, _owner.Stat.GetDamage());
    }
}