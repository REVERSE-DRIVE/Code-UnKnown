public class KineticSkill : Skill
{
    public override bool UseSkill()
    {
        if (base.UseSkill()) return false;
        
        return true;
    }
}