using EnemyManage;

public class LockerAttackState : EnemyAttackState
{
    public LockerAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}