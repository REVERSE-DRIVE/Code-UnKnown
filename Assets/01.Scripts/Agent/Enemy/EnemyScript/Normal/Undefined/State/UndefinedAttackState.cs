using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class UndefinedAttackState : EnemyAttackState
{
    public UndefinedAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
    }
}