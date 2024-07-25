using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class UndefinedAttackState : EnemyAttackState
{
    public UndefinedAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        var projectile =
            PoolingManager.Instance.Pop(PoolingType.Projectile_Tracing) as TracingProjectile;
        projectile.transform.position = _enemyBase.transform.position;
        Vector3 dir = _enemyBase.targetTrm.position - _enemyBase.transform.position;
        projectile.Shoot(dir);
        
    }
}