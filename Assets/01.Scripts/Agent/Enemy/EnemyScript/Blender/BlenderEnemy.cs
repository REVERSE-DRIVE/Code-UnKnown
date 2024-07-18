using EnemyManage;

// 플레이어를 향해 다가오며 주위의 톱니바퀴를 돌림
public class BlenderEnemy : EnemyBase
{
    public SpikeGearSpinManager SpikeGearSpinManager { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        SpikeGearSpinManager = GetComponentInChildren<SpikeGearSpinManager>();
        StateMachine.AddState(EnemyStateEnum.Idle, new BlenderIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(this, StateMachine, "Chase"));
        StateMachine.AddState(EnemyStateEnum.Attack, new EnemyAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Dead, new BlenderDeadState(this, StateMachine, "Dead"));
    }
}