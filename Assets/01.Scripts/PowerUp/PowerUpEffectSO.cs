using UnityEngine;

public abstract class PowerUpEffectSO : ScriptableObject
{
    public string code;
    public PowerUpEffectType type;
    
    public abstract void UseEffect();
    public abstract bool CanUpgradeEffect();
    


}
