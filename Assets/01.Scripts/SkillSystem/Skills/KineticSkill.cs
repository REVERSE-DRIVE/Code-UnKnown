using UnityEngine;

public class KineticSkill : AttackEffectStateSkill
{
    public int effectRate = 10;
    
    protected override void HandlePlayerAttackEvent()
    {
        if (Random.Range(0, 100) < effectRate)
        {
            base.HandlePlayerAttackEvent();
            
        }
    }
}