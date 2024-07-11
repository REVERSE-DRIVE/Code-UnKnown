using System;

public class SlowEffect : Effect
{
    public SlowEffect(Agent owner, int level, float duration) : base(owner, level, duration)
    {
        
    }
    
    public override void Enter()
    {
        _owner.Stat.moveSpeed -= 5;
    }
    
    

    protected override void UpdateEffect()
    {
    }

    public override void Exit()
    {
       
        _owner.Stat.moveSpeed += 5;
    }

    
}