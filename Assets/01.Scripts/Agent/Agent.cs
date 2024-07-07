using System;
using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    #region Components List
    
    public Animator AnimatorCompo { get; protected set; }
    public AgentMovement MovementCompo { get; protected set; }
    public AgentVFX VFXCompo { get; protected set; }
    public AgentEffectController EffectCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }

    #endregion

    protected Transform _visualTrm;
    protected SpriteRenderer _spriteRenderer;
    public bool CanStateChangeable { get; protected set; } = true;
    public bool isDead;

    
    
    // Agent
    [field: SerializeField] public AgentStat Stat { get; protected set; }
    
    protected virtual void Awake()
    {
        MovementCompo = GetComponent<AgentMovement>();
        MovementCompo.Initialize(this);
        VFXCompo = transform.Find("AgentVFX").GetComponent<AgentVFX>();
        EffectCompo = GetComponent<AgentEffectController>();
        HealthCompo = GetComponent<Health>();
        _visualTrm = transform.Find("Visual");
        AnimatorCompo = _visualTrm.GetComponent<Animator>();
        _spriteRenderer = _visualTrm.GetComponent<SpriteRenderer>();
        // Player는 SpriteRenderer가 여러곳에 분산되어있기 때문에
        // Agent에 만드는건 SOLID위반이 됨, 나중에 Enemy로 옮겨야함
        Stat = Instantiate(Stat);

        HealthCompo.OnHealthChangedEvent += HandleHitEvent;
    }
    
    public Coroutine StartDelayCallback(float time, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(time, Callback));
    }

    protected IEnumerator DelayCoroutine(float time, Action Callback)
    {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }
    
    public abstract void SetDead();
    
    private void HandleHitEvent()
    { // SOLID위반으로 인해 나중에 Enemy쪽으로 읾길 수도있음
        
        
    }

    private IEnumerator HitCoroutine()
    {
        
        yield return new WaitForSeconds(0.1f);
    }
}
