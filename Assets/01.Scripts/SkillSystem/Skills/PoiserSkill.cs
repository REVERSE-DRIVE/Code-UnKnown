using EffectState;
using UnityEngine;

public class PoiserSkill : AttackTriggerSkill
{
    [SerializeField] private EffectStateController _targetController;
    
    public override bool UseSkill()
    {
        if (base.UseSkill()) return false;
        
        return true;
    }

    protected override void HandlePlayerAttackEvent()
    {
        if (player.PlayerAttackCompo.currentTargetTrm.TryGetComponent(out EffectStateController effectController))
        {
            _targetController = effectController;
            _targetController.ApplyEffect(EffectState.EffectType.DataLose);
        }
        
    }
}