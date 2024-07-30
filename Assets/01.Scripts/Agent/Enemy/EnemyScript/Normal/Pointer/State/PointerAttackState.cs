using System.Collections;
using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class PointerAttackState : EnemyAttackState
{
    private Coroutine _shotCoroutine;
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
        yield return new WaitForSeconds(1f);
        var projectile =
            PoolingManager.Instance.Pop(PoolingType.Projectile_Tracing) as TracingProjectile;
        projectile.transform.position = _enemyBase.transform.position;
        Vector3 dir = _enemyBase.targetTrm.position - _enemyBase.transform.position;
        projectile.Shoot(dir);
    }
}