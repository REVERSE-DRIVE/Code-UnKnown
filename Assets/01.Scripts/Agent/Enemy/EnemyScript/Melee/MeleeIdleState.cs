using EnemyManage;
using UnityEngine;

public class MeleeIdleState : EnemyState<MeleeStateEnum>
{
    public MeleeIdleState(Enemy enemyBase, EnemyStateMachine<MeleeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
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
            _stateMachine.ChangeState(MeleeStateEnum.Chase);
        }
    }
}