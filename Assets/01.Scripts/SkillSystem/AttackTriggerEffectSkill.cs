using UnityEngine;
using UnityEngine.Serialization;

public abstract class AttackTriggerEffectSkill : AttackTriggerSkill
{
    public int effectTime = 3;
    public int buffAmount = 10;

    [SerializeField] private StatType _type;
    private Stat _defStat;
    private float _leftTimer;
    private bool _isActive;
    protected override void Start()
    {
        base.Start();
        _defStat = player.Stat.GetStat(_type);
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
                HandleEffectReset();
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
            HandleEffectUp();
        }
            
        _isActive = true;
        _leftTimer = effectTime;
        
    }


    private void HandleEffectUp()
    {
        _defStat.AddModifier(buffAmount);
    }

    private void HandleEffectReset()
    {
        _defStat.RemoveModifier(buffAmount);
    }

}