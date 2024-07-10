using System.Collections;
using EnemyManage;
using ObjectPooling;
using UnityEngine;

public class EnemyBase : Enemy, IPoolable
{
    [field:SerializeField] public PoolingType type { get; set; }
    [SerializeField] private Material _hitMaterial;
    private Material _defaultMaterial;
    public GameObject ObjectPrefab => gameObject;
    public EnemyStateMachine<EnemyStateEnum> StateMachine { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<EnemyStateEnum>();
        _defaultMaterial = _spriteRenderer.material;
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
        Debug.Log("Enemy Dead");
        StateMachine.ChangeState(EnemyStateEnum.Dead);
    }
    
    public void SetHitMaterial()
    {
        StartCoroutine(ChangeMaterial());
    }

    private IEnumerator ChangeMaterial()
    {
        _spriteRenderer.material = _hitMaterial;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.material = _defaultMaterial;
    }

    public void ResetItem()
    {
        HealthCompo.Initialize(Stat.health);
        isDead = false;
        StateMachine.Initialize(EnemyStateEnum.Idle, this);
        _spriteRenderer.material = _defaultMaterial;
    }
}