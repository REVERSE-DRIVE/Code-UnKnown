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
        var bullet = PoolingManager.Instance.Pop(PoolingType.TestPoolingEffect) as TracingProjectile;
        bullet.transform.position = _enemyBase.transform.position;
        bullet.Shoot(_enemyBase.targetTrm.position - _enemyBase.transform.position);
    }
}