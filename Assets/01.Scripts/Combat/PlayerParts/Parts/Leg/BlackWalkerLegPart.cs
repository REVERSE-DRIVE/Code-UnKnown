using System.Collections;
using PlayerPartsManage;
using UnityEngine;

public class BlackWalkerLegPart : PlayerPart
{
    [SerializeField] private LayerMask _whatIsEnemy;
    [SerializeField] private float _attackRadius;
    private Collider2D[] _hits = new Collider2D[10];
    private int blackOutCount = 0;
    private Coroutine _blackOutCoroutine;
    
    public BlackWalkerLegPart(Player owner) : base(owner)
    {
    }

    public override void OnMount()
    {
        _blackOutCoroutine = StartCoroutine(Walk());
    }

    public override void OnUnMount()
    {
        StopCoroutine(_blackOutCoroutine);
    }

    private IEnumerator Walk()
    {
        while (true)
        {
            if (_owner.PlayerController.Velocity.magnitude > 0)
            {
                if (blackOutCount >= 100 && _owner.PlayerAttackCompo.IsAttacking)
                {
                    DamageCast();
                    blackOutCount = 0;  
                }
                yield return new WaitForSeconds(1f);
                blackOutCount += 20;
            }

            yield return null;
        }
    }

    private void DamageCast()
    {
        int cnt = Physics2D.OverlapCircleNonAlloc(_owner.transform.position, _attackRadius, _hits, _whatIsEnemy);
        
        if (cnt > 0)
        {
            for (int i = 0; i < cnt; i++)
            {
                if (_hits[i].TryGetComponent(out EnemyBase enemy))
                {
                    enemy.OnFaint(2f);
                }
            }
        }
    }
}