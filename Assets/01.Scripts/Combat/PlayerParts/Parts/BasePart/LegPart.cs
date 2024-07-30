using UnityEngine;

namespace PlayerPartsManage
{
    public class LegPart : PlayerPart
    {
        private SpriteRenderer[] _spriteRenderers;
        public LegPart(Player owner) : base(owner)
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