using EnemyManage;

public class SentinelAttackState : EnemyAttackState
{
    public SentinelAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}