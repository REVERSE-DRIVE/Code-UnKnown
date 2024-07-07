using EnemyManage;
using UnityEngine;

public class JailIdleState : EnemyIdleState
{
    public JailIdleState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
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