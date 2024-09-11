using System.Collections;
using CombatSkillManage;
using PlayerPartsManage;
using UnityEngine;

public class StackerLegPart : PlayerPart
{
    [SerializeField] private PlayerSkillSO _skill;
    public StackerLegPart(Player owner) : base(owner)
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