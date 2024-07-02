using EnemyManage;

public class TypeAUpgradeEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(this, StateMachine, "Chase"));
        StateMachine.AddState(EnemyStateEnum.Attack, new TypeAUpgradeAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(this, StateMachine, "Dead"));   
    }
    
    private void Start()
    {
        StateMachine.Initialize(EnemyStateEnum.Idle, this);
    }
}