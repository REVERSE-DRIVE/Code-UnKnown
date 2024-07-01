using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerPartsManage
{
    public class PlayerPart : MonoBehaviour
    {
        protected Player _owner;
        
        
        public PlayerPart(Player owner)
        {
            _owner = owner;
        }
    
    }

}