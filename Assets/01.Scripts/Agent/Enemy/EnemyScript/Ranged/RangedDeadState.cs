using EnemyManage;

public class RangedDeadState : EnemyState<RangedStateEnum>
{
    public RangedDeadState(Enemy enemyBase, EnemyStateMachine<RangedStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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