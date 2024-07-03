using System.Collections;
using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class TypeAUpgradeAttackState : EnemyAttackState
{
    public TypeAUpgradeAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _enemyBase.StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.5f);
        var laser = PoolingManager.Instance.Pop(PoolingType.EnemyLaser) as EnemyLaser;
        laser.transform.position = _enemyBase.transform.position;
        laser.Shot(_enemyBase.targetTrm.position);
    }
}