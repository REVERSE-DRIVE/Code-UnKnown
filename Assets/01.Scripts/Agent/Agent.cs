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
        AnimatorCompo = GetComponent<Animator>();

        Stat = Instantiate(Stat);
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
}
