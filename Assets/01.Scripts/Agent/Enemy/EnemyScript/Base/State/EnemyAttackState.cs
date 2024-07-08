using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class EnemyAttackState : EnemyState<EnemyStateEnum>
{
    public EnemyAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if (_endTriggerCalled)
        {
            _enemyBase.StopAllCoroutines();
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }
}