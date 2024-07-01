using EnemyManage;

public class TypeAEnemy : Enemy
{
    public EnemyStateMachine<MeleeStateEnum> StateMachine { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<MeleeStateEnum>();
        
        StateMachine.AddState(MeleeStateEnum.Idle, new MeleeIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(MeleeStateEnum.Chase, new MeleeChaseState(this, StateMachine, "Chase"));
        StateMachine.AddState(MeleeStateEnum.Attack, new MeleeAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(MeleeStateEnum.Dead, new MeleeDeadState(this, StateMachine, "Dead"));   
    }
    
    private void Start()
    {
        StateMachine.Initialize(MeleeStateEnum.Idle, this);
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