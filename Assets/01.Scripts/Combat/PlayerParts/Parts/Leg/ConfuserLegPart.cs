using PlayerPartsManage;

public class ConfuserLegPart : PlayerPart
{
    private int _attackPower = 0;
    private int dashSpeed = 0;
    public ConfuserLegPart(Player owner) : base(owner)
    {
    }

    public override void OnMount()
    {
        _owner.PlayerInputCompo.IsReverse = true;
        _attackPower = _owner.Stat.GetDamage();
        _owner.Stat.damage.AddModifier(_attackPower * 20 / 100);
        //dashSpeed = _owner.PlayerAttackCompo.
        //_owner.DashSpeed += DashSpeed * 20 / 100;
    }

    public override void OnUnMount()
    {
        _owner.PlayerInputCompo.IsReverse = false;
        _owner.Stat.damage.RemoveModifier(_attackPower * 20 / 100);
        //_owner.DashSpeed -= DashSpeed * 20 / 100;
    }
}