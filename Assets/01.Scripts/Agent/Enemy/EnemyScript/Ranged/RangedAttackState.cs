using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class RangedAttackState : EnemyState<RangedStateEnum>
{
    public RangedAttackState(Enemy enemyBase, EnemyStateMachine<RangedStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        var bullet = PoolingManager.Instance.Pop(PoolingType.TestPoolingEffect) as TracingProjectile;
        bullet.transform.position = _enemyBase.transform.position;
        bullet.Shoot(_enemyBase.targetTrm.position - _enemyBase.transform.position);
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        
        if (_endTriggerCalled)
        {
            Debug.Log("Attack");
            _stateMachine.ChangeState(RangedStateEnum.Idle);
        }
    }
}