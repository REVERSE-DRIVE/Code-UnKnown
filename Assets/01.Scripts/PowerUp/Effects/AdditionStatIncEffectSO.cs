using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/PowerUp/Effect/AddStatInc")]
public class AdditionStatIncEffectSO : PowerUpEffectSO
{
    public AddStatType targetType;
    public int increaseValue;
    public override void UseEffect()
    {
        PlayerManager.Instance.player.additionalStat.AddModifier(targetType, increaseValue);
    }

    public override bool CanUpgradeEffect()
    {
        return true;
    }
}