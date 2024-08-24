﻿using System;
using UnityEngine;

public class DecoySword : MonoBehaviour
{
    [SerializeField]private DecoyEnemy _owner;

    public void Attack()
    {
        _owner.DamageCasterCompo.CastDamage(3f, _owner.Stat.GetDamage());
    } 
}