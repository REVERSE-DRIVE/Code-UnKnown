using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerHoldEffect : MonoBehaviour
{
    [Header("Blading Setting")]
    [SerializeField] private ParticleSystem _bladingParticle;
    [SerializeField] private Transform _rangeCircleTrm;

    [Header("Dash Setting")]
    [SerializeField] private ParticleSystem _readyParticle;

    [SerializeField] private Transform _dashImpactTrm;
    [SerializeField] private float _dashImpactSize;
    [SerializeField] private float _impactDuration = 0.2f;

    private Sequence _seq;
    
    #region Range Blading
    
    public void PlayBlading(float duration)
    {
        StartCoroutine(BladingCoroutine(duration));
    }

    private IEnumerator BladingCoroutine(float duration)
    {
        _bladingParticle.Play();
        yield return new WaitForSeconds(duration);
        _bladingParticle.Stop();
    }

    public IEnumerator RangeSizeUp(float size, float duration)
    {
        return RangeCoroutine(size, duration);
    }

    private IEnumerator RangeCoroutine(float size, float duration)
    {
        _rangeCircleTrm.gameObject.SetActive(true);
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newSize = Mathf.Lerp(0, size, currentTime / duration);
            _rangeCircleTrm.localScale = new Vector2(newSize,newSize);
            yield return null;
        }
        _rangeCircleTrm.localScale = new Vector2(size,size);
        _rangeCircleTrm.gameObject.SetActive(false);
    }

    #endregion

    #region Dash Attack


    public void PlayDashReady()
    {
        _readyParticle.Play();
    }

    public void PlayDashImpact(Vector2 direction)
    {
        _dashImpactTrm.right = direction;
        _seq = DOTween.Sequence();
        _seq.Append(_dashImpactTrm.DOScaleY(_dashImpactSize, _impactDuration));
        _seq.Append(_dashImpactTrm.DOScaleY(0, 0.05f));
    }

    #endregion

}