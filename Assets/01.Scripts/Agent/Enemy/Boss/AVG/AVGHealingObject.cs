﻿using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class AVGHealingObject : DestroyableObject
{
    public bool isActive = true;
    [SerializeField] private ParticleSystem _generateParticle;
    [SerializeField] private PoolingType _destoryParticle;
    private SpriteRenderer _spriteRenderer;
    
    protected void Awake()
    {
        _generateParticle = GetComponentInChildren<ParticleSystem>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        OnDestroyEvent += HandleDestroy;
    }
    

    protected void HandleDestroy()
    {
        EffectObject effectObject = PoolingManager.Instance.Pop(_destoryParticle) as EffectObject;
        effectObject.Initialize(transform.position);
        _spriteRenderer.color = Color.red;
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
        _spriteRenderer.color = Color.white;
        _currentHealth = _maxHealth;
    }

    public void Destroy()
    {
        HandleDestroy();
        isActive = false;
        gameObject.SetActive(false);
    }
}