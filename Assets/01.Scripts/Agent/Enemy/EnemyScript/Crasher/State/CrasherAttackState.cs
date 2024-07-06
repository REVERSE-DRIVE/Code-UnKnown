using EnemyManage;

public class CrasherAttackState : EnemyAttackState
{
    public CrasherAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}