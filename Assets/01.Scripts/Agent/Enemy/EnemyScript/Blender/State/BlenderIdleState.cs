using EnemyManage;

public class BlenderIdleState : EnemyIdleState
{
    private BlenderEnemy BlenderEnemy => (BlenderEnemy) _enemyBase;
    public BlenderIdleState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        BlenderEnemy.GearSpinManager.StartSpin();
    }
}