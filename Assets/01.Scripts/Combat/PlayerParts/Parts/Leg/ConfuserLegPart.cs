using PlayerPartsManage;
using UnityEngine;

public class ConfuserLegPart : PlayerPart
{
    private int _attackPower = 0;
    private int dashSpeed = 0;
    public ConfuserLegPart(Player owner) : base(owner)
    {
    }

    public override void UseSkill()
    {
        _owner.PlayerInputCompo.OnMovementEvent += Confuse;
        _attackPower = _owner.Stat.GetDamage();
        _owner.Stat.damage.AddModifier(_attackPower * 20 / 100);
        //dashSpeed = _owner.DashSpeed;
        //_owner.DashSpeed += DashSpeed * 20 / 100;
    }

    private void Confuse(Vector2 moveVector)
    {
        moveVector = new Vector2(-moveVector.x, -moveVector.y);
    }
}