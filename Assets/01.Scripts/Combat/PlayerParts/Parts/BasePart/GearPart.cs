using UnityEngine;

namespace PlayerPartsManage
{
    public class GearPart : PlayerPart
    {
        private SpriteRenderer _spriteRenderer;

        public override void Init()
        {
            base.Init();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public GearPart(Player owner) : base(owner)
        {
        }

        public override void OnMount()
        {
        }

        public override void OnUnMount()
        {
            
        }
    }
}