using UnityEngine;

namespace PlayerPartsManage
{
    public class GearPart : PlayerPart
    {
        private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public GearPart(Player owner) : base(owner)
        {
        }

        public override void UseSkill()
        {
        }
    }
}