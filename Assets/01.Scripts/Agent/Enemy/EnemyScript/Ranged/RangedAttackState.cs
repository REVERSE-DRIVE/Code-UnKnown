using EnemyManage;

public class RangedAttackState : EnemyState<RangedStateEnum>
{
    public RangedAttackState(Enemy enemyBase, EnemyStateMachine<RangedStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}