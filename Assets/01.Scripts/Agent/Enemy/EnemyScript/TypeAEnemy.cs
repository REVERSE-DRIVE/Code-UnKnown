using EnemyManage;

public class TypeAEnemy : Enemy
{
    public EnemyStateMachine<EnemyStateEnum> StateMachine { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<EnemyStateEnum>();
        
        StateMachine.AddState(EnemyStateEnum.Idle, new EnemyIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(EnemyStateEnum.Chase, new EnemyChaseState(this, StateMachine, "Chase"));
        StateMachine.AddState(EnemyStateEnum.Attack, new TypeAAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(EnemyStateEnum.Dead, new EnemyDeadState(this, StateMachine, "Dead"));   
    }
    
    private void Start()
    {
        StateMachine.Initialize(EnemyStateEnum.Idle, this);
    }
    
    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }
    
    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }
}