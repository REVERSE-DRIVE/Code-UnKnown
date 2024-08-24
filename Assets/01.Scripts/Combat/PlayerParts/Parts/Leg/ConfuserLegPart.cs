using PlayerPartsManage;

public class ConfuserLegPart : PlayerPart
{
    private int _attackPower = 0;
    private int _dashSpeed = 0;
    public ConfuserLegPart(Player owner) : base(owner)
    {
    }

    public override void OnMount()
    {
        _attackPower = _owner.Stat.GetDamage();
        _dashSpeed = _owner.additionalStat.dashSpeed.GetValue();
        _owner.Stat.damage.AddModifier(_attackPower * 20 / 100);
        _owner.additionalStat.dashSpeed.AddModifier(_dashSpeed * 20 / 100);
    }

    public override void OnUnMount()
    {
        _owner.Stat.damage.RemoveModifier(_attackPower * 20 / 100);
        _owner.additionalStat.dashSpeed.RemoveModifier(_dashSpeed * 20 / 100);
    }
}