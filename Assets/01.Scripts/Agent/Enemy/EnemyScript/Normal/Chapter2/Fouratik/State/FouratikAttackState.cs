using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class FouratikAttackState : EnemyAttackState
{
    public FouratikAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        ShotBullet(Vector2.up);
        ShotBullet(Vector2.down);
        ShotBullet(Vector2.left);
        ShotBullet(Vector2.right);
    }
    
    private void ShotBullet(Vector2 direction)
    {
        var bullet = PoolingManager.Instance.Pop(PoolingType.Projectile_Normal) as Projectile;
        bullet.Initialize(_enemyBase.transform.position, _enemyBase.Stat.GetDamage(), 5, 5);
        bullet.Shoot(direction);
        
    }
}