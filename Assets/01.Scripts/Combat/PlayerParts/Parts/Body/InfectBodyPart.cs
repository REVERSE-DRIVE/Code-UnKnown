﻿using PlayerPartsManage;
using UnityEngine;

public class InfectBodyPart : BodyPart
{
    public InfectBodyPart(Player owner) : base(owner)
    {
    }
    
    public override void UseSkill()
    {
        _owner.PlayerAttackCompo.OnAttackEvent += Attack;
    }

    private void Attack()
    {
        int random = Random.Range(0, 100);
        if (random < 5)
        {
            
        }
    }
}