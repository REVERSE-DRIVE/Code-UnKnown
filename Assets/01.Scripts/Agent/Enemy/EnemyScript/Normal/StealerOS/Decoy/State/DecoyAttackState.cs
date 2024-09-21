using System.Collections;
using EnemyManage;
using UnityEngine;

public class DecoyAttackState : EnemyAttackState
{
    private DecoyEnemy _decoyEnemy;
    private Coroutine _attackCoroutine;
    public DecoyAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _decoyEnemy = enemyBase as DecoyEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        _decoyEnemy.SwordCompo.Attack();
    }
}