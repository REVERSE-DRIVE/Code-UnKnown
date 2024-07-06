using EnemyManage;

public class EnemyDeadState : EnemyState<EnemyStateEnum>
{
    public EnemyDeadState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _enemyBase.SetDead();
        }
    }
}