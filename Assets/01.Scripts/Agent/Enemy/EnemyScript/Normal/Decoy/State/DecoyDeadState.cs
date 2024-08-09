using EnemyManage;
using UnityEngine;

public class DecoyDeadState : EnemyDeadState
{
    private float _radius = 4f;
    private Collider2D[] _colliders = new Collider2D[10];
    public DecoyDeadState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    protected override void Die()
    {
        base.Die();
        int count = Physics2D.OverlapCircleNonAlloc
            (_enemyBase.transform.position, _radius, _colliders, _enemyBase.WhatIsPlayer);
        for (int i = 0; i < count; i++)
        {
            if (_colliders[i].TryGetComponent(out Player player))
            {
                player.HealthCompo.TakeDamage(_enemyBase.Stat.GetDamage());
            }
            
        }
    }
}