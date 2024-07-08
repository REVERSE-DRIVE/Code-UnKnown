using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class EnemyBase : Enemy, IPoolable
{
    [field:SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;
    public EnemyStateMachine<EnemyStateEnum> StateMachine { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<EnemyStateEnum>();
    }
    
    public virtual void Start()
    {
        StateMachine.Initialize(EnemyStateEnum.Idle, this);
        HealthCompo.Initialize(Stat.health);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }
    
    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    public override void SetDead()
    {
        base.SetDead();
        isDead = true;
        StateMachine.ChangeState(EnemyStateEnum.Dead);
    }

    public void ResetItem()
    {
        HealthCompo.Initialize(Stat.health);
        isDead = false;
        StateMachine.ChangeState(EnemyStateEnum.Idle);
    }
}