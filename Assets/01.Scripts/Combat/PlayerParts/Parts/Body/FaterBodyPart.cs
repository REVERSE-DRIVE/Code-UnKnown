using PlayerPartsManage;

public class FaterBodyPart : BodyPart
{
    public FaterBodyPart(Player owner) : base(owner)
    {
    }
    
    public override void UseSkill()
    {
        int maxHealth = _owner.HealthCompo.maxHealth;
        int damage = _owner.Stat.GetDamage();
        _owner.HealthCompo.maxHealth -= maxHealth * 10 / 100;
        _owner.Stat.damage.AddModifier(damage * 20 / 100);
    }
}