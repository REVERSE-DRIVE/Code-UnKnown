using System.Collections;
using EnemyManage;
using UnityEngine;

public class EnemyChaseState : EnemyState<EnemyStateEnum>
{
    public EnemyChaseState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        float distance = (_enemyBase.targetTrm.position - _enemyBase.transform.position).magnitude;
        
        if (distance <= _enemyBase.chaseDistance && distance > _enemyBase.attackDistance)
        {
            Debug.Log("Chase");
            _enemyBase.MovementCompo.StopImmediately();
            _enemyBase.MovementCompo.SetMovement(_enemyBase.targetTrm.position - _enemyBase.transform.position);
            
        }
        else if (distance <= _enemyBase.attackDistance)
        {
            Debug.Log("Attack");
            _stateMachine.ChangeState(EnemyStateEnum.Attack);
        }
        else
        {
            _stateMachine.ChangeState(EnemyStateEnum.Idle);
        }
    }
    
    
}