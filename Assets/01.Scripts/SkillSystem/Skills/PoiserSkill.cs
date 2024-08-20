using UnityEngine;

public class PoiserSkill : AttackTriggerSkill
{
    public int effectTime = 3;
    public int buffPercent = 5;
    
    private Stat _atkBonusStat;
    private float _leftTimer;
    private bool _isActive;

    protected override void Start()
    {
        base.Start();
        _atkBonusStat = player.Stat.bonusAtk;
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
                HandleAtkReset();
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
            HandleAtkUp();
        }
            
        _isActive = true;
        _leftTimer = effectTime;
        
    }


    private void HandleAtkUp()
    {
        _atkBonusStat.AddModifier(buffPercent);
    }

    private void HandleAtkReset()
    {
        _atkBonusStat.RemoveModifier(buffPercent);
    }
    
}