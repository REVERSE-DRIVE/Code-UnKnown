using System;
using Random = UnityEngine.Random;

public class PermomentSkill : AttackTriggerSkill
{
    private PlayerComboCounter _comboCounter;
    public int bonusComboRate = 20;
    public int bonusAmount = 1;

    protected override void Start()
    {
        base.Start();
        _comboCounter = player.transform.GetComponent<PlayerComboCounter>();
    }

    protected override void HandlePlayerAttackEvent()
    {
        base.HandlePlayerAttackEvent();
        ApplyBonus();
        
    }

    private void ApplyBonus()
    {
        if (Random.Range(0, 100) < bonusComboRate)
        {
            _comboCounter.BonusCombo(bonusAmount);
        }    
    }
}