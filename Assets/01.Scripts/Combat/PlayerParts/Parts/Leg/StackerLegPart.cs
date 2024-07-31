using System.Collections;
using PlayerPartsManage;

public class StackerLegPart : PlayerPart
{
    private int stackCount = 0;
    public StackerLegPart(Player owner) : base(owner)
    {
    }

    public override void OnMount()
    {
        StartCoroutine(StackCount());
    }

    public override void OnUnMount()
    {
        StopAllCoroutines();
    }

    private IEnumerator StackCount()
    {
        while (true)
        {
            if (_owner.PlayerController.Velocity.magnitude > 0)
            {
                //if (_owner.IsDash)
                {
                    if (stackCount >= 10)
                    {
                        // 3초간 적 기절
                        // enemy 20% 추가 피해
                        stackCount = 0;
                    }
                    stackCount++;
                }
            }
            yield return null;
        }
    }
}