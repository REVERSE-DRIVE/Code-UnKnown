using UnityEngine;
using EnemyManage;

public class CommonEnemy : Enemy
{
    public EnemyStateMachine<CommonStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<CommonStateEnum>();
        
        StateMachine.AddState(CommonStateEnum.Idle, new CommonIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(CommonStateEnum.Battle, new CommonBattleState(this, StateMachine, "Battle"));
        StateMachine.AddState(CommonStateEnum.Attack, new CommonAttackState(this, StateMachine, "Attack"));
        StateMachine.AddState(CommonStateEnum.Dead, new CommonDeadState(this, StateMachine, "Dead"));   
    }

    private void Start()
    {
        StateMachine.Initialize(CommonStateEnum.Idle, this);
    }

    private void Update()
    {
        Debug.Log(StateMachine.CurrentState);
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    public override void SetDead()
    {
        base.SetDead();
    }
}