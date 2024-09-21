using EnemyManage;
using UnityEngine;

public class InducedEleteSkillState : EnemySkillState
{
    private Collider2D[] _colliders = new Collider2D[10];
    public InducedEleteSkillState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    protected override void TakeSkill()
    {
        Debug.Log("Induced Elete Skill");
        int count = Physics2D.OverlapCircleNonAlloc(_enemyBase.transform.position, 3, _colliders, _enemyBase.WhatIsPlayer);
        Debug.Log(count);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                if (_colliders[i].TryGetComponent(out Player player))
                {
                    player.HealthCompo.TakeDamage(10);
                }
            }
        }
    }
}