﻿using System.Collections;
using EnemyManage;
using UnityEngine;

public class RangedIdleState : EnemyState<RangedStateEnum>
{
    private EnemyMovement _movementCompo;
    public RangedIdleState(Enemy enemyBase, EnemyStateMachine<RangedStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _movementCompo = _enemyBase.MovementCompo as EnemyMovement;
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        _enemyBase.StartCoroutine(ChaseRoutine());
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        Collider2D target = _enemyBase.IsPlayerDetected();
        if (target == null) return;
        
        Vector3 direction = target.transform.position - _enemyBase.transform.position;
        
        if (!_enemyBase.IsObstacleDetected(direction.magnitude, direction.normalized))
        {
            _enemyBase.StopAllCoroutines();
            _enemyBase.targetTrm = target.transform;
            _stateMachine.ChangeState(RangedStateEnum.Chase);
        }
    }
    
    private IEnumerator ChaseRoutine()
    {
        while (true)
        {
            Debug.Log("Chaseaaaaaaaaaaaaaaaaaa");
            Vector3 randomDir = _movementCompo.GetRandomPosition();
            _enemyBase.MovementCompo.SetMovement(randomDir);
            yield return new WaitForSeconds(0.5f);
            _enemyBase.MovementCompo.StopImmediately();
            yield return new WaitForSeconds(0.5f);
        }
    }
}