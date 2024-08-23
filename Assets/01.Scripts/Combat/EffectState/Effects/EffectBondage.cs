using ObjectManage;
using ObjectPooling;
using UnityEngine;

namespace EffectState
{
    public class EffectBondage : EffectState
    {
        private int _buffValue;
        private EffectObject effect;
        public EffectBondage(Agent agent, bool isResist) : base(agent, isResist)
        {
        }

        public override void Enter()
        {
            _owner.MovementCompo.isStun = true;
            _buffValue = effectLevel;
            _owner.Stat.moveSpeed.AddModifier(-_buffValue);
            effect = PoolingManager.Instance.Pop(PoolingType.StunVFX) as EffectObject;
            effect.Initialize(_owner.transform.position);
            effect.Play();
        }

        protected override void UpdateState()
        {
            effect.Initialize(_owner.transform.position);
        }

        protected override void UpdateStateBySecond()
        {
        }

        public override void Over()
        {
            if(effect != null)
                PoolingManager.Instance.Push(effect);
            _owner.Stat.moveSpeed.RemoveModifier(-_buffValue);
            _owner.MovementCompo.isStun = false;
        }

        public override void ResetEffect()
        {
            base.ResetEffect();
            if(effect != null)
                PoolingManager.Instance.Push(effect);
            if(_owner.MovementCompo != null)
                _owner.MovementCompo.isStun = false;
        }
    }
}