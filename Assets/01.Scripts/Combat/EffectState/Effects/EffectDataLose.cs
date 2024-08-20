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
        }

        public override void Over()
        {
            
        }

        
    }
}