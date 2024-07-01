using EnemyManage;
using UnityEngine;

public class MeleeAttackState : EnemyState<MeleeStateEnum>
{
    public MeleeAttackState(Enemy enemyBase, EnemyStateMachine<MeleeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            Debug.Log("Attack");
            _stateMachine.ChangeState(MeleeStateEnum.Idle);
        }
    }
}