using EnemyManage;

public class RangedIdleState : EnemyState<RangedStateEnum>
{
    public RangedIdleState(Enemy enemyBase, EnemyStateMachine<RangedStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}