﻿using System;

public class SlowEffect : Effect
{
    public SlowEffect(Agent owner, int level, float duration) : base(owner, level, duration)
    {
        
    }
    
    public override void Enter()
    {
        
    }
    
    

    protected override void UpdateEffect()
    {
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    
}