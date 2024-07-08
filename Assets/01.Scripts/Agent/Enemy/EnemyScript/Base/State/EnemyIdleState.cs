using System.Collections;
using EnemyManage;
using UnityEngine;

public class EnemyIdleState : EnemyState<EnemyStateEnum>
{
    private EnemyMovement _movementCompo;
    public EnemyIdleState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _movementCompo = _enemyBase.MovementCompo as EnemyMovement;
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        Collider2D target = _enemyBase.IsPlayerDetected();
        if (target == null) return;
        
        Vector3 direction = target.transform.position - _enemyBase.transform.position;
        
        if (!_enemyBase.IsObstacleDetected(direction.magnitude, direction.normalized))
        {
            _enemyBase.StopAllCoroutines();
            _enemyBase.targetTrm = target.transform;
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
        }
    }
    
    
}