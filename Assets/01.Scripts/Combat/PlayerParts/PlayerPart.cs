using System;
using UnityEngine;

namespace PlayerPartsManage
{
    public abstract class PlayerPart : MonoBehaviour
    {
        protected Player _owner;

        protected virtual void Awake()
        {
        }


        public PlayerPart(Player owner)
        {
            _owner = owner;
        }

        public abstract void UseSkill();

    }

}