public class CommonDeadState : EnemyState<CommonStateEnum>
{
    public CommonDeadState(Enemy enemyBase, EnemyStateMachine<CommonStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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