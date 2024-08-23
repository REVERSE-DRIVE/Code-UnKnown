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
            Debug.Log("스턴에 걸림");
            _owner.MovementCompo.isStun = true;
            _buffValue = effectLevel;
            _owner.Stat.moveSpeed.AddModifier(-_buffValue);
            effect = PoolingManager.Instance.Pop(PoolingType.StunVFX) as EffectObject;
            effect.Initialize(_owner.transform.position);
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
            _owner.MovementCompo.isStun = false;
        }
    }
}