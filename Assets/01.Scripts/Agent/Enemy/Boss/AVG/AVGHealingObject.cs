using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class AVGHealingObject : DestroyableObject
{
    public bool isActive = true;
    [SerializeField] private ParticleSystem _generateParticle;
    [SerializeField] private PoolingType _destoryParticle;
    
    protected void Awake()
    {
        _generateParticle = GetComponentInChildren<ParticleSystem>();

        OnDestroyEvent += HandleDestroy;
    }
    

    protected void HandleDestroy()
    {
        EffectObject effectObject = PoolingManager.Instance.Pop(_destoryParticle) as EffectObject;
        effectObject.Initialize(transform.position);
        effectObject.Play();
    }

    public void OnCore()
    {
        gameObject.SetActive(true);
        _generateParticle.Play();
        SetDefault();
    }

    private void SetDefault()
    {
        isActive = true;
        _currentHealth = _maxHealth;
    }

    public void Destroy()
    {
        HandleDestroy();
        isActive = false;
        gameObject.SetActive(false);
    }
}