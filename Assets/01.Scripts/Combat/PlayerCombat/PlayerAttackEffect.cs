using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerAttackEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _attackParticle;
    private bool _isPlaying;
    [SerializeField] private Transform _impactTrm;
    [SerializeField] private float _impactSize = 0.6f;

    [Header("Setting")] 
    [SerializeField] private float _impactDuration = 0.3f;

    private Sequence _seq;
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
    
    private IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(0.3f);

    }
}
