using EnemyManage;

public class TypeAEnemy : Enemy
{
    public EnemyStateMachine<RangedStateEnum> StateMachine { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<RangedStateEnum>();
        
        StateMachine.AddState(RangedStateEnum.Idle, new RangedIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(RangedStateEnum.Chase, new RangedChaseState(this, StateMachine, "Chase"));
        StateMachine.AddState(RangedStateEnum.Attack, new RangedAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(RangedStateEnum.Dead, new RangedDeadState(this, StateMachine, "Dead"));   
    }
    
    private void Start()
    {
        StateMachine.Initialize(RangedStateEnum.Idle, this);
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