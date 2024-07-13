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
    private bool isInitEnd;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<EnemyStateEnum>();
        _defaultMaterial = _spriteRenderer.material;
        isInitEnd = false;
    }
    
    public virtual void Start()
    {
        Init();
    }

    private void Init()
    {
        isInitEnd = false;
        _spriteRenderer.material = _defaultMaterial;
        if (_spriteRenderer.material == _hitMaterial)
            _spriteRenderer.material = _defaultMaterial;
        StateMachine.Initialize(EnemyStateEnum.Idle, this);
        isInitEnd = true;
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
        if (isDead) return;
        if (isInitEnd)
            StartCoroutine(ChangeMaterial());
    }

    private IEnumerator ChangeMaterial()
    {
        _spriteRenderer.material = _hitMaterial;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.material = _defaultMaterial;
    }

    public void ResetItem()
    {
        isDead = false;
        Init();
    }
}