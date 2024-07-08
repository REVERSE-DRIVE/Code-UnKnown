using System.Collections;
using DG.Tweening;
using EnemyManage;
using UnityEngine;

public class UndefinedChaseState : EnemyChaseState
{
    public UndefinedChaseState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        _enemyBase.StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _enemyBase.RendererCompo.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _enemyBase.transform.position = _enemyBase.targetTrm.position;
        yield return new WaitForSeconds(3f);
        _stateMachine.ChangeState(EnemyStateEnum.Attack);
    }
}