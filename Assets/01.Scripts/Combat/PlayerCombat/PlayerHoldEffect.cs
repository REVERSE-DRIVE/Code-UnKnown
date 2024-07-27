using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHoldEffect : MonoBehaviour
{
    [Header("Blading Setting")]
    [SerializeField] private ParticleSystem _bladingParticle;
    [SerializeField] private Transform _rangeCircleTrm;
    
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
}