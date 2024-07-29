using PlayerPartsManage;
using UnityEngine;

public class InfectBodyPart : PlayerPart
{
    public InfectBodyPart(Player owner) : base(owner)
    {
    }
    
    public override void OnMount()
    {
        _owner.PlayerAttackCompo.OnAttackEvent += Attack;
    }

    public override void OnUnMount()
    {
        _owner.PlayerAttackCompo.OnAttackEvent -= Attack;
    }

    private void Attack()
    {
        int random = Random.Range(0, 100);
        if (random < 5)
        {
            // 3초간 적 기절
        }
    }
}