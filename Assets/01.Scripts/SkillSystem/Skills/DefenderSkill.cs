using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class DefenderSkill : AttackCountSkill
{
    public int effectTime = 3;
    public int buffPercent = 20;
    
    private int _buffValue;
    private Stat _defStat;
    private float _leftTimer;
    private bool _isActive;
    protected override void Start()
    {
        base.Start();
        _defStat = player.Stat.damageResist;
    }

    protected override void Update()
    {
        base.Update();
        if (_isActive)
        {
            _leftTimer -= Time.deltaTime;
            if (_leftTimer < 0)
            {
                _isActive = false;
                HandleDefenceReset();
            }
        }
    }

    public override bool UseSkill()
    {
        if (base.UseSkill()) return false;
        
        return true;
    }
    

    protected override void HandlePlayerAttackEvent()
    {
        if (!_isActive)
        {
            HandleDefenceUp();
        }
            
        _isActive = true;
        _leftTimer = effectTime;
        
    }


    private void HandleDefenceUp()
    {
        _defStat.AddModifier(buffPercent);
    }

    private void HandleDefenceReset()
    {
        _defStat.RemoveModifier(buffPercent);
    }

    
}