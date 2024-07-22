using UnityEngine;

namespace PlayerPartsManage
{
    public class LegPart : PlayerPart
    {
        [field:SerializeField] private SpriteRenderer[] _spriteRenderers;
        
        public SpriteRenderer[] SpriteRenderers => _spriteRenderers;
        
        public LegPart(Player owner) : base(owner)
        {
        }

        public override void UseSkill()
        {
            throw new System.NotImplementedException();
        }
    }
}