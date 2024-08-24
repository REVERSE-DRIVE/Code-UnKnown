using System.Collections;
using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class PointerAttackState : EnemyAttackState
{
    private Coroutine _shotCoroutine;
    private float _waitTime = 1f;
    private float _bulletSpeed = 10f;
    private float _bulletLifeTime = 3f;
    public PointerAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _shotCoroutine = _enemyBase.StartCoroutine(ShotCoroutine());
    }

    private IEnumerator ShotCoroutine()
    {
        yield return new WaitForSeconds(_waitTime);
        var projectile =
            PoolingManager.Instance.Pop(PoolingType.Projectile_Reflect) as ReflectProjectile;
        projectile.Initialize(_enemyBase.transform.position, _damage, _bulletSpeed, _bulletLifeTime);
        Vector3 dir = (_enemyBase.targetTrm.position - _enemyBase.transform.position).normalized;
        projectile.Shoot(dir);
    }
}