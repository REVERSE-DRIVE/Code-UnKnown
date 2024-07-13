using System;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Items/PowerUp/Effect/UnLockSkill")]
public class UnLockSkillEffectSO : PowerUpEffectSO
{
    public PlayerSkill unLockSkill;
    
    public override void UseEffect()
    {
        Skill skill = SkillManager.Instance.GetSkill(unLockSkill);
        skill.UnlockSkill();
    }

    public override bool CanUpgradeEffect()
    {
        Skill skill = SkillManager.Instance.GetSkill(unLockSkill);
        return skill.skillEnabled == false;
    }

    private void OnValidate()
    {
        type = EffectType.SkillUnlock;
    }
}
