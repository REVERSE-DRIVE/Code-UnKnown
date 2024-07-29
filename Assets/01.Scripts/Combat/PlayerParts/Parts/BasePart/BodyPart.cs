using UnityEngine;

namespace PlayerPartsManage
{
    public class BodyPart : PlayerPart
    {
        private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public BodyPart(Player owner) : base(owner)
        {
        }

        public override void OnMount()
        {
            _spriteRenderer.color = Color.red;
        }

        public override void OnUnMount()
        {
            
        }
    }
}