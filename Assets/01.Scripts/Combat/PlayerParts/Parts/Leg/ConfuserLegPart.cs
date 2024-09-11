using CombatSkillManage;
using PlayerPartsManage;
using UnityEngine;

public class ConfuserLegPart : PlayerPart
{
    [SerializeField] private PlayerSkillSO _skill;
    public ConfuserLegPart(Player owner) : base(owner)
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