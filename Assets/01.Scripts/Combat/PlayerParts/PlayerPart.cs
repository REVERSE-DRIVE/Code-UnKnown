using System;
using UnityEngine;

namespace PlayerPartsManage
{
    public abstract class PlayerPart : MonoBehaviour
    {
        [SerializeField] protected Player _owner;
        [SerializeField] protected PlayerPartDataSO _data;

        protected virtual void Awake()
        {
            _owner = PlayerManager.Instance.player;
        }


        public PlayerPart(Player owner)
        {
            _owner = owner;
        }

        public abstract void OnMount();
        public abstract void OnUnMount();
    }

}