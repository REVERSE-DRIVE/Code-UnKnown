using EnemyManage;

public class HarpyAttackState : EnemyAttackState
{
    public HarpyAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}