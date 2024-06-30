using System.Collections;
using UnityEngine;
using EnemyManage;

public class CommonIdleState : EnemyState<CommonStateEnum>
{
    public CommonIdleState(Enemy enemyBase, EnemyStateMachine<CommonStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
        
        Collider2D target = _enemyBase.IsPlayerDetected();
        if (target == null) return;
        
        Vector3 direction = target.transform.position - _enemyBase.transform.position;
        
        if (!_enemyBase.IsObstacleDetected(direction.magnitude, direction.normalized))
        {
            _enemyBase.targetTrm = target.transform;
            _stateMachine.ChangeState(CommonStateEnum.Battle);
        }
    }
}