using EnemyManage;

public class MeltingAttackState : EnemyAttackState
{
    public MeltingAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    
}