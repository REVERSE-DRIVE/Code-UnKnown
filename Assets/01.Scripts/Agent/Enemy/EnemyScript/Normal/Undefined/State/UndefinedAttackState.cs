using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class UndefinedAttackState : EnemyAttackState
{
    // Bullet Setting
    private int _damage;
    private float _speed = 10f;
    private float _lifeTime = 3f;
    
    public UndefinedAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        Vector3 dir = (_enemyBase.targetTrm.transform.position - _enemyBase.transform.position).normalized;
        Vector2 dir2 = Quaternion.Euler(0, 0, 30) * dir;
        Vector3 dir3 = Quaternion.Euler(0, 0, -30) * dir;
        Shot(dir);
        Shot(dir2);
        Shot(dir3);
    }

    private void Shot(Vector3 dir)
    {
        var bullet = PoolingManager.Instance.Pop(PoolingType.Projectile_Spining) as SpinProjectile;
        bullet.Initialize(_enemyBase.transform.position, _damage, 5, 3f);
        bullet.Shoot(dir);
    }
}