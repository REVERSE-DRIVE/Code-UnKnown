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
        _enemyBase.RigidCompo.AddForce((_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized * 10, ForceMode2D.Impulse);
    }
}