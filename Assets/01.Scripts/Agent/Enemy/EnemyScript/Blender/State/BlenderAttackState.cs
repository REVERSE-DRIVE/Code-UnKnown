using EnemyManage;

public class BlenderAttackState : EnemyAttackState
{
    private BlenderEnemy BlenderEnemy => (BlenderEnemy) _enemyBase;
    public BlenderAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        BlenderEnemy.GearSpinManager.StartSpin();
    }
}