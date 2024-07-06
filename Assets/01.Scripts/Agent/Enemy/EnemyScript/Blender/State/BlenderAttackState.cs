using EnemyManage;

public class BlenderAttackState : EnemyAttackState
{
    public BlenderAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}