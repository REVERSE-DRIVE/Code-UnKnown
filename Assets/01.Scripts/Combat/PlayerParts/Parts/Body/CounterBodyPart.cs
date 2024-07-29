﻿using PlayerPartsManage;

public class CounterBodyPart : PlayerPart
{
    private int count = 0;
    public CounterBodyPart(Player owner) : base(owner)
    {
    }
    
    public override void OnMount()
    {
        _owner.HealthCompo.OnHealthChangedValueEvent += Shock;
    }

    public override void OnUnMount()
    {
        _owner.HealthCompo.OnHealthChangedValueEvent -= Shock;
    }

    private void Shock(int prevvalue, int newvalue, int max)
    {
        if (prevvalue > newvalue)
        {
            count++;
        }
        if (count >= 3)
        {
            // 충격파 발사
            count = 0;
        }
    }
}