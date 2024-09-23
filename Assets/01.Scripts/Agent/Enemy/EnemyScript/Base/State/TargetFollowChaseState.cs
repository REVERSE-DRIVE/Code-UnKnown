using System.Collections;
using EnemyManage;
using UnityEngine;

public class TargetFollowChaseState : EnemyChaseState
{
    public TargetFollowChaseState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName, float chaseTime) : base(enemyBase, stateMachine, animBoolName, chaseTime)
    {
    }

    public override void Enter()
    {
        _enemy.StartCoroutine(ChaseRoutine());
    }

    protected override IEnumerator ChaseRoutine()
    {
        if (_isChase) yield break;
        _isChase = true;
        Transform targetTrm = _enemy.targetTrm;
        if (targetTrm == null)
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
            yield break;
        }

        WaitForSeconds ws = new WaitForSeconds(_chaseTime);

        while (true)
        {
            if (Vector3.Distance(_enemy.transform.position, _enemy.targetTrm.position) < _enemy.attackDistance &&
                Time.time - _enemyBase.lastAttackTime >= _enemy.attackCooldown)
            {
                _stateMachine.ChangeState(EnemyStateEnum.Attack);
                yield break;
            }
            _enemyBase.MovementCompo.SetMovement(targetTrm.position);
            _movementCompo.LookToTarget(targetTrm.position);
            yield return ws;
            _enemyBase.MovementCompo.StopImmediately();
            yield return ws;
        }
    }
}