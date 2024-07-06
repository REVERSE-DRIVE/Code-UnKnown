using EnemyManage;
using UnityEngine;

public class CrasherAttackState : EnemyAttackState
{
    public CrasherAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        _enemyBase.RigidCompo.AddForce(_enemyBase.targetTrm.forward * 1000f, ForceMode2D.Impulse);
    }
}