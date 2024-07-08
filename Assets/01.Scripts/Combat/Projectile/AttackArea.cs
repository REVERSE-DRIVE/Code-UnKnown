using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _decreasedSpeedDuration;
    [SerializeField] private float _decreasedSpeed;
    [SerializeField] private Vector3 _scale;
    
    private bool _isAttacking;
    
    public void SetActive(bool value)
    {
        DOTween.Kill(transform);
        if (value)
        {
            gameObject.SetActive(true);
            transform.DOScale(_scale, 0.5f);
        }
        else
        {
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => gameObject.SetActive(false));
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            _isAttacking = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            StartCoroutine(Attack(health));
        }
    }

    private IEnumerator Attack(Health health)
    {
        if (_isAttacking) yield break;
        _isAttacking = true;
        WaitForSeconds wait = new WaitForSeconds(1f);
        StartCoroutine(DecreaseSpeed(_decreasedSpeed));
        while (_isAttacking)
        {
            health.TakeDamage(_damage);
            yield return wait;
        }
    }

    private IEnumerator DecreaseSpeed(float decreasedSpeed)
    {
        float currentTime = 0;
        while (currentTime < _decreasedSpeedDuration)
        {
            currentTime += Time.deltaTime;
            // Decrease speed
            yield return null;
        }
        
        StartCoroutine(IncreaseSpeed(decreasedSpeed));
    }
    
    private IEnumerator IncreaseSpeed(float increasedSpeed)
    {
        float currentTime = 0;
        while (currentTime < _decreasedSpeedDuration)
        {
            currentTime += Time.deltaTime;
            // Increase speed
            yield return null;
        }
    }
}