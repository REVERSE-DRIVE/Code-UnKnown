using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerAttackEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _attackParticle;
    private bool _isPlaying;
    [SerializeField] private Transform _impactTrm;
    [SerializeField] private Transform _targetMarkTrm;
    [SerializeField] private float _impactSize = 0.6f;
    private TrailRenderer _trailRenderer;

    [Header("Setting")] 
    [SerializeField] private float _impactDuration = 0.3f;

    private Sequence _seq;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Play(Vector2 direction)
    {
        _impactTrm.right = direction;
        _impactTrm.position = transform.position;
        CameraManager.Instance.Shake(14f, 0.13f);
        PlayImpact();
    }

    private void PlayImpact()
    {
        _seq = DOTween.Sequence();
        _seq.Append(_impactTrm.DOScaleY(_impactSize, _impactDuration));
        _seq.Append(_impactTrm.DOScaleY(0, 0.05f));

    }

    public void SetTargetActive(bool value)
    {
        _targetMarkTrm.gameObject.SetActive(value);
    }
    public void SetTarget(Vector2 position)
    {
        SetTargetActive(true);
        _targetMarkTrm.position = position;
    }

    public void SetTrailActive(bool value)
    {
        _trailRenderer.enabled = value;
    }
    
}
