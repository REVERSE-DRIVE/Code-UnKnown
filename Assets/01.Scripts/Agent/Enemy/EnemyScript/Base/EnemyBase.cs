using System;
using System.Collections;
using EnemyManage;
using ObjectPooling;
using QuestManage;
using UnityEngine;

public class EnemyBase : Enemy, IPoolable
{
    public EnemyStateMachine<EnemyStateEnum> StateMachine { get; protected set; }
    #region Pooling Setting
    [field:SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;
    #endregion
    
    #region Attack Setting
    [Header("Attack Setting")]
    [SerializeField] private Material _hitMaterial;
    private Material _defaultMaterial;
    private Coroutine _faintCoroutine;
    private bool isHit;
    public bool IsFaint { get; private set; }
    #endregion
    
    #region Anothor Setting
    private bool isInitEnd;
    [field:SerializeField] public bool IsElete { get; protected set; }
    [SerializeField] private EnemyType _enemyType;
    #endregion

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
        HealthCompo.OnHealthChangedEvent.AddListener(SetHitMaterial);
        HealthCompo.OnDieEvent.AddListener(SetDead);
    }

    private void Init()
    {
        isInitEnd = false;
        _spriteRenderer.material = _defaultMaterial;
        ColliderCompo.enabled = true;
        StateMachine.Initialize(EnemyStateEnum.Idle, this);
        HealthCompo.SetHealth(Stat.maxHealth.GetValue());
        CanStateChangeable = true;
        isInitEnd = true;
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }
    
    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    /// <summary>
    /// Chang To Dead State
    /// </summary>
    public override void SetDead()
    {
        Debug.Log("Enemy Dead");
        if (QuestObserver.Instance == null) Debug.Log("QuestObserver is Null");
        if (QuestObserver.Instance.questCounter == null) Debug.Log("QuestCounter is Null");
        StateMachine.ChangeState(EnemyStateEnum.Dead);
        QuestObserver.Instance.Trigger(QuestType.Kill, QuestObserver.Instance.questCounter.AddEnemyKillCount());
        CanStateChangeable = false;
        
        base.SetDead();
    }
    
    /// <summary>
    /// Change Material When Hit
    /// </summary>
    public void SetHitMaterial()
    {
        if (isDead) return;
        Vibration.Vibrate(200);
        if (isInitEnd)
            StartCoroutine(ChangeMaterial());
    }

    private IEnumerator ChangeMaterial()
    {
        if (isHit) yield break;
        isHit = true;
        _spriteRenderer.material = _hitMaterial;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.material = _defaultMaterial;
        if (_spriteRenderer.material == _hitMaterial)
        {
            _spriteRenderer.material = _defaultMaterial;
        }
        isHit = false;
        
        if (_spriteRenderer.material == _hitMaterial)
        {
            _spriteRenderer.material = _defaultMaterial;
        }
    }
    
    /// <summary>
    /// Change Faint State When Hit
    /// </summary>
    /// <param name="duration">Faint duration</param>
    public void OnFaint(float duration)
    {
        if (IsElete) return;
        Debug.Log("Faint");
        _faintCoroutine = StartCoroutine(FaintCoroutine(duration));
    }

    private IEnumerator FaintCoroutine(float duration)
    {
        Debug.Log("FaintCoroutine");
        if (IsFaint) yield break;
        if (_faintCoroutine != null)
        {
            StopCoroutine(_faintCoroutine);
        }
        StateMachine.ChangeState(EnemyStateEnum.Idle);
        IsFaint = true;
        MovementCompo.StopImmediately();
        for (int i = 0; i < 3; i++)
        {
            RigidCompo.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
            RigidCompo.AddForce(Vector2.left * 5, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(duration - 0.6f);
        StateMachine.ChangeState(EnemyStateEnum.Chase);
        IsFaint = false;
        _faintCoroutine = null;
    }

    public void ResetItem()
    {
        isDead = false;
        Init();
    }

}