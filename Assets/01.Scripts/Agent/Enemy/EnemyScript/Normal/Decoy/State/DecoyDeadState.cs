using EnemyManage;
using UnityEngine;

public class DecoyDeadState : EnemyDeadState
{
    public DecoyDeadState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    protected override void Die()
    {
        base.Die();
    }
}