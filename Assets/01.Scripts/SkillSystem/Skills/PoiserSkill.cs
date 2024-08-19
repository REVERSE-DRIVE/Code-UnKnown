using EffectState;
using UnityEngine;

public class PoiserSkill : Skill
{
    [SerializeField] private EffectStateController _targetController;
    
    public override bool UseSkill()
    {
        if (base.UseSkill()) return false;
        
        return true;
    }
}