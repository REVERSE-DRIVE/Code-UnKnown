using EnemyManage;

public class DestroyerAttackState : EnemyAttackState
{
    public DestroyerAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}