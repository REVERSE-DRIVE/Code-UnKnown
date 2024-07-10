using EnemyManage;
using UnityEngine;

public class EnemyDeadState : EnemyState<EnemyStateEnum>
{
    public EnemyDeadState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("Dead State");
        if (_endTriggerCalled)
        {
            Debug.Log("Enemy Dead");
            _enemyBase.isDead = true;
        }
    }
}