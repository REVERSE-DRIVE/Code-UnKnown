using EnemyManage;

public class EnemyBase : Enemy
{
    public EnemyStateMachine<EnemyStateEnum> StateMachine { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<EnemyStateEnum>();
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