using System.Collections;
using PlayerPartsManage;
using UnityEngine;

public class StackerLegPart : PlayerPart
{
    [SerializeField] private LayerMask _whatIsEnemy;
    private int stackCount = 0;
    private Coroutine _stackCountCoroutine;
    private Collider2D[] _hits = new Collider2D[10];
    public StackerLegPart(Player owner) : base(owner)
    {
    }

    public override void OnMount()
    {
        _stackCountCoroutine = StartCoroutine(StackCount());
    }

    public override void OnUnMount()
    {
        StopCoroutine(_stackCountCoroutine);
    }

    private IEnumerator StackCount()
    {
        while (true)
        {
            if (_owner.PlayerController.Velocity.magnitude > 0)
            {
                if (_owner.PlayerAttackCompo.IsAttacking)
                {
                    if (stackCount >= 10)
                    {
                        // 3초간 적 기절
                        // enemy 20% 추가 피해
                        Attack();
                        stackCount = 0;
                    }
                    stackCount++;
                }
            }
            yield return null;
        }
    }

    private void Attack()
    {
        int damage = _owner.Stat.GetDamage();
        int cnt = Physics2D.OverlapCircleNonAlloc(_owner.transform.position, 3, _hits, _whatIsEnemy);
        if (cnt <= 0) return;
        for (int i = 0; i < cnt; i++)
        {
            if (_hits[i].TryGetComponent(out EnemyBase enemy))
            {
                enemy.OnFaint(3f);
                enemy.HealthCompo.TakeDamage(damage * 20 / 100);
            }
        }
    }
}