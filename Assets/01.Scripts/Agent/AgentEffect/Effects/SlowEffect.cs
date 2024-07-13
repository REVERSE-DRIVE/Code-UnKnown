using System;

public class SlowEffect : Effect
{
    public SlowEffect(Agent owner, int level, float duration) : base(owner, level, duration)
    {
        
    }
    
    public override void Enter()
    {
        _owner.Stat.AddModifier(StatType.MoveSpeed, -level);    
    }
    
    

    protected override void UpdateEffect()
    {
    }

    public override void Exit()
    {
        _owner.Stat.RemoveModifier(StatType.MoveSpeed, -level);       
    }

    
}