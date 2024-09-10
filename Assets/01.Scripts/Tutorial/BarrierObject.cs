using System.Collections;
using System.Collections.Generic;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class BarrierObject : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private GameObject _barrierVisual;
    private Dictionary<int, string> _dictionary;
    
    public void Disable()
    {
        if (_dictionary.TryGetValue(1, out string ming))
        {
            // 키 값이 있을때 out으로 받은 ming으로 뭔가 해주면 됨
        }
        else
        {
            // 예외 처리가능
        }
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
