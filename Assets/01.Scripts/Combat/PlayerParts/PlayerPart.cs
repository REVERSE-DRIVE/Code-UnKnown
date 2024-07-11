using System;
using UnityEngine;

namespace PlayerPartsManage
{
    public abstract class PlayerPart : MonoBehaviour
    {
        protected Player _owner;
        protected SpriteRenderer _spriteRenderer;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        public PlayerPart(Player owner)
        {
            _owner = owner;
        }

        public abstract void UseSkill();

    }

}