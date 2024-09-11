using System;
using CombatSkillManage;
using UnityEngine;

namespace PlayerPartsManage
{
    public abstract class PlayerPart : MonoBehaviour
    {
        [SerializeField] protected Player _owner;
        [SerializeField] protected PlayerPartDataSO _data;
        protected PlayerSkillController playerSkillController;

        public virtual void Init()
        {
            _owner = PlayerManager.Instance.player;
            playerSkillController = _owner.transform.GetChild(5).GetComponent<PlayerSkillController>();
        }


        public PlayerPart(Player owner)
        {
            _owner = owner;
        }

        public abstract void OnMount();
        public abstract void OnUnMount();
    }

}