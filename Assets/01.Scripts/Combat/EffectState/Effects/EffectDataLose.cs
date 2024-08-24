using ObjectManage;
using ObjectPooling;

namespace EffectState
{
    public class EffectDataLose : EffectState
    {
        public EffectDataLose(Agent agent, bool isResist) : base(agent, isResist)
        {
        }
        public override void Enter()
        {
            
        }

        protected override void UpdateState()
        {
            
        }

        protected override void UpdateStateBySecond()
        {
            _owner.HealthCompo.TakeDamage(effectLevel);
            EffectObject effect = PoolingManager.Instance.Pop(PoolingType.PoiserHitVFX) as EffectObject;
            effect.Initialize(_owner.transform.position);
        }

        public override void Over()
        {
            
        }

        
    }
}