using EnemyManage;

public class RangedChaseState : EnemyState<RangedStateEnum>
{
    public RangedChaseState(Enemy enemyBase, EnemyStateMachine<RangedStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}