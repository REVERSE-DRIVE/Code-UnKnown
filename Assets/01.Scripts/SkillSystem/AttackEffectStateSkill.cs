using EffectState;
using UnityEngine;

public class AttackEffectStateSkill : AttackTriggerSkill
{
    [SerializeField] private EffectStateController _targetController;
    [SerializeField] private EffectType _effectType;
    public int level = 2;
    public float duration = 3;
    
    public override bool UseSkill()
    {
        if (base.UseSkill()) return false;
        
        return true;
    }

    protected override void HandlePlayerAttackEvent()
    {
        base.HandlePlayerAttackEvent();
        if (player.PlayerAttackCompo.currentTargetTrm.TryGetComponent(out EffectStateController effectController))
        {
            _targetController = effectController;
            _targetController.ApplyEffect(_effectType, level, duration);
        }
        
    }
}