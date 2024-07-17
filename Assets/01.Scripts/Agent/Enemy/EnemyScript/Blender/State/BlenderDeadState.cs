using EnemyManage;

public class BlenderDeadState : EnemyDeadState
{
    private BlenderEnemy BlenderEnemy => (BlenderEnemy) _enemyBase;
    public BlenderDeadState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _enemyBase.MovementCompo.StopImmediately();
        BlenderEnemy.SpikeGearSpinManager.StopRotation();
    }
}