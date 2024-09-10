using System.Collections;
using System.Collections.Generic;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class BarrierObject : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private GameObject _barrierVisual;
    
    public void Disable()
    {
        StartCoroutine(DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        EffectObject effect = PoolingManager.Instance.Pop(PoolingType.PlayerAppearVFX) as EffectObject;
        effect.Initialize(transform.position);
        yield return new WaitForSeconds(_delay);
        _barrierVisual.SetActive(false);

    }

}
