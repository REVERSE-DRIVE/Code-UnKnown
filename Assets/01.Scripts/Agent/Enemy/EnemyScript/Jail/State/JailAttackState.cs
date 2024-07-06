using EnemyManage;

public class JailAttackState : EnemyAttackState
{
    public JailAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}