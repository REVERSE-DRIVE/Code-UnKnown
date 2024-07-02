﻿using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class EnemyAttackState : EnemyState<EnemyStateEnum>
{
    public EnemyAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        
        if (_endTriggerCalled)
        {
            Debug.Log("Attack");
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }
}