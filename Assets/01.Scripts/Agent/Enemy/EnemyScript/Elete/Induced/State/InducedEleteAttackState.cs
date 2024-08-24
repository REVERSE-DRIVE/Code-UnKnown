using System.Collections;
using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class InducedEleteAttackState : EnemyAttackState
{
    public InducedEleteAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        for (int i = 0; i < 4; i++)
        {
            var bullet = PoolingManager.Instance.Pop(PoolingType.Projectile_Tracing) as TracingProjectile;
            bullet.Initialize(_enemyBase.transform.position, _damage, 10f, 1f);
            bullet.Shoot(_enemyBase.targetTrm.position - _enemyBase.transform.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
}