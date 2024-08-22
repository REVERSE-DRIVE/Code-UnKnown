namespace EffectState
{
    public class EffectBondage : EffectState
    {
        private int _buffValue;
        public EffectBondage(Agent agent, bool isResist) : base(agent, isResist)
        {
        }

        public override void Enter()
        {
            _buffValue = effectLevel;
            _owner.Stat.moveSpeed.AddModifier(-_buffValue);
        }

        protected override void UpdateState()
        {
        }

        protected override void UpdateStateBySecond()
        {
        }

        public override void Over()
        {
            _owner.Stat.moveSpeed.RemoveModifier(-_buffValue);
        }
    }
}