using System;
using UnityEngine;

public class ImmediateEffectSO : PowerUpEffectSO
{
    public ImmediateEffectType effectType;
    public int effectValue;
    
    public override void UseEffect()
    {
        switch (effectType)
        {
            case ImmediateEffectType.Heal:
                PlayerManager.Instance.player.HealthCompo.RestoreHealth(effectValue);
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