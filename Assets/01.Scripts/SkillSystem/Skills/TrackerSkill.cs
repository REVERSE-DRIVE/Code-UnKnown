public class TrackerSkill: Skill
{
    public override bool UseSkill()
    {
        if (base.UseSkill()) return false;
        
        return true;
    }
}