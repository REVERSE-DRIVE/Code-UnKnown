using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/PowerUp/Effect/ImmediateEffect")]
public class ImmediateEffectSO : PowerUpEffectSO
{
    public ImmediateEffectType effectType;
    public int effectValue;
    private Player _player;
    
    public override void UseEffect()
    {
        if (_player == null) _player = PlayerManager.Instance.player;
        
        switch (effectType)
        {
            case ImmediateEffectType.HealByPercent:
                int health = (int)((float)_player.HealthCompo.maxHealth / 100 * effectValue);
                _player.HealthCompo.RestoreHealth(health);
                break;
            case ImmediateEffectType.NormalHeal:
                _player.HealthCompo.RestoreHealth(effectValue);
                break;
            default:
                Debug.LogWarning($"처리되지 않은 예외 효과입니다 [effectType:{effectType.ToString()}]");
                break;
        }
    }

    public override bool CanUpgradeEffect()
    {
        return true;
    }
}