using EnemyManage;

public class TypeBAttackState : EnemyAttackState
{
    public TypeBAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}