using System;
using EnemyManage;
using UnityEngine;

public class InducedEleteEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(this, StateMachine, "Chase", 2f));
        StateMachine.AddState(EnemyStateEnum.Attack, new InducedEleteAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Skill, new InducedEleteSkillState(this, StateMachine, "Skill"));
        StateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(this, StateMachine, "Dead"));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 3f);
        Gizmos.color = Color.white;
    }
}