using EnemyManage;

public class SupportAttackState : EnemyAttackState
{
    public SupportAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}