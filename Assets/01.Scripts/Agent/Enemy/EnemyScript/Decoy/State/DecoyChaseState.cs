﻿using EnemyManage;

public class DecoyChaseState : EnemyChaseState
{
    public DecoyChaseState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        _movementCompo.LookToTarget(_enemyBase.targetTrm.position);
        float distance = (_enemyBase.targetTrm.position - _enemyBase.transform.position).magnitude;
        
        if (distance <= _enemyBase.chaseDistance && distance > _enemyBase.attackDistance)
        {
            _enemyBase.StopAllCoroutines();
            _isChase = false;
            _enemyBase.MovementCompo.StopImmediately();
            _enemyBase.MovementCompo.SetMovement(_enemyBase.targetTrm.position - _enemyBase.transform.position);
            
        }
        else if (distance <= _enemyBase.attackDistance)
        {
            _enemyBase.StopAllCoroutines();
            _isChase = false;
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
        else
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }
}