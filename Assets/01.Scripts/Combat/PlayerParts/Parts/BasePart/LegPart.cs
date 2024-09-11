using CombatSkillManage;
using UnityEngine;

namespace PlayerPartsManage
{
    public class LegPart : PlayerPart
    {
        [SerializeField] private PlayerSkillSO _skill;
        public LegPart(Player owner) : base(owner)
        {
        }

        public override void OnMount()
        {
            Debug.Log(playerSkillController);
            Debug.Log("daddds"+_skill);
            playerSkillController.ChangeSkill(_skill);
        }

        public override void OnUnMount()
        {
        }
    }
}