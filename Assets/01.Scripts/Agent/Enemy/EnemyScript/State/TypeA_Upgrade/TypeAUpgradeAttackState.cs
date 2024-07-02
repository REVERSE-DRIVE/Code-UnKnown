using EnemyManage;
using ObjectPooling;

public class TypeAUpgradeAttackState : EnemyAttackState
{
    public TypeAUpgradeAttackState(Enemy enemyBase, EnemyStateMachine<EnemyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        
    }
}