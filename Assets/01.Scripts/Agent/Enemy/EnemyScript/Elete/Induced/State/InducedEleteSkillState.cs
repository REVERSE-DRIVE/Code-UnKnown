using EnemyManage;
using UnityEngine;

public class InducedEleteSkillState : EnemySkillState
{
    private Collider[] _colliders;
    public InducedEleteSkillState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _colliders = new Collider[10];
    }

    protected override void TakeSkill()
    {
        int count = Physics.OverlapSphereNonAlloc(_enemyBase.transform.position, 3f, _colliders, _enemyBase.WhatIsPlayer);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                if (_colliders[i].TryGetComponent(out Health health))
                {
                    if (_colliders[i].CompareTag("Player"))
                    {
                        health.TakeDamage(_enemyBase.Stat.GetDamage());
                    }
                }
            }
        }
    }
}