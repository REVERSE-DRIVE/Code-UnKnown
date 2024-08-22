using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class EnemyAttackState : EnemyState<EnemyStateEnum>
{
    private EnemyMovement _movementCompo;
    public EnemyAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _movementCompo = _enemyBase.MovementCompo as EnemyMovement;
    }

    public override void Enter()
    {
        base.Enter();
        _movementCompo.LookToTarget(_enemyBase.targetTrm.position);
        _enemyBase.MovementCompo.StopImmediately();
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        if (_endTriggerCalled)
        {
            AttackEnd();
        }
    }

    protected virtual void AttackEnd()
    {
        Debug.Log("Attack End");
        //_enemyBase.StopAllCoroutines();
        _stateMachine.ChangeState(EnemyStateEnum.Idle);
    }
}