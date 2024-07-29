using PlayerPartsManage;

public class FaterBodyPart : PlayerPart
{
    private int _maxHealth;
    private int _damage;
    private int _decreaseHealth;
    private int _increaseDamage;
    
    public FaterBodyPart(Player owner) : base(owner)
    {
    }
    
    public override void OnMount()
    {
        _maxHealth = _owner.HealthCompo.maxHealth;
        _damage = _owner.Stat.GetDamage();
        _decreaseHealth = _maxHealth * 10 / 100;
        _increaseDamage = _damage * 20 / 100;
        _owner.HealthCompo.maxHealth -= _decreaseHealth;
        _owner.Stat.damage.AddModifier(_increaseDamage);
    }

    public override void OnUnMount()
    {
        _owner.HealthCompo.maxHealth += _decreaseHealth;
        _owner.Stat.damage.RemoveModifier(_increaseDamage);
    }
}