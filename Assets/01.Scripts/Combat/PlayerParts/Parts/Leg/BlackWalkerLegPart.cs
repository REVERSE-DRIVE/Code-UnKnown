using System.Collections;
using PlayerPartsManage;
using UnityEngine;

public class BlackWalkerLegPart : PlayerPart
{
    private int blackOutCount = 0;
    public BlackWalkerLegPart(Player owner) : base(owner)
    {
    }

    public override void UseSkill()
    {
        StartCoroutine(Walk());
    }

    private IEnumerator Walk()
    {
        while (true)
        {
            
            if (_owner.PlayerController.Velocity.magnitude > 0)
            {
                if (blackOutCount >= 100 /*&& 공격했으면*/)
                {
                    // 2초간 적 기절
                }
                yield return new WaitForSeconds(1f);
                blackOutCount += 20;
            }

            yield return null;
        }
    }
}