using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    [SerializeField] private OverlapDamageCaster _damageCaster;
    [SerializeField] private int _damage;
    [SerializeField] private float _damageTick;

    [ContextMenu("T")]
    private void Test()
    {
        GetDamage(5, 5);
    }

    public void GetDamage(int radius, float time)
    {
        transform.DOScale(new Vector3(radius * 2, radius * 2, 1), 0.5f);
        StartCoroutine(GetDamageCoroutine(radius, time));
    }

    private IEnumerator GetDamageCoroutine(int radius, float time)
    {
        float timer = 0;
        float damageTickTimer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime;
            damageTickTimer += Time.deltaTime;
            if (damageTickTimer >= _damageTick)
            {
                _damageCaster.CastDamage(radius, _damage);
                Debug.Log("Cast");
                damageTickTimer = 0;
            }

            yield return null;
        }

        transform.DOScale(Vector3.zero, 0.5f);
    }
}