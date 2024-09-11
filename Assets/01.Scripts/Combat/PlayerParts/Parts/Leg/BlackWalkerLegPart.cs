using System.Collections;
using CombatSkillManage;
using PlayerPartsManage;
using UnityEngine;

public class BlackWalkerLegPart : PlayerPart
{
    [SerializeField] PlayerSkillSO _skill;
    
    public BlackWalkerLegPart(Player owner) : base(owner)
    {
    }

    public override void OnMount()
    {
        playerSkillController.ChangeSkill(_skill);
    }

    public override void OnUnMount()
    {
    }
}