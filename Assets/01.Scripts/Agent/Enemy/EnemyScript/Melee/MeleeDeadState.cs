using EnemyManage;

public class MeleeDeadState : EnemyState<MeleeStateEnum>
{
    public MeleeDeadState(Enemy enemyBase, EnemyStateMachine<MeleeStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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