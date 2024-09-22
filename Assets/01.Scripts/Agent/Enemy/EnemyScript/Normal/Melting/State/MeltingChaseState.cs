using EnemyManage;

public class MeltingChaseState : EnemyChaseState
{
    public MeltingChaseState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName, float chaseTime) : base(enemyBase, stateMachine, animBoolName, chaseTime)
    {
    }

    public override void Enter()
    {
    }
}