using EnemyManage;
using ObjectPooling;

public class TypeAAttackState : EnemyAttackState
{
    public TypeAAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        var bullet = PoolingManager.Instance.Pop(PoolingType.Projectile_Tracing) as TracingProjectile;
        bullet.Initialize(_enemyBase.transform.position, _damage, 2f, 1f);
        bullet.Shoot(_enemyBase.targetTrm.position - _enemyBase.transform.position);
    }
}