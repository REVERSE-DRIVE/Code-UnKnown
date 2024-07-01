using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerPartsManage
{
    public abstract class PlayerPart : MonoBehaviour
    {
        protected Player _owner;
        
        
        public PlayerPart(Player owner)
        {
            _owner = owner;
        }

        public abstract void UseSkill();

    }

}