using EnemyManage;

public class TypeBUpgradeAttackState : EnemyAttackState
{
    public TypeBUpgradeAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}