using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class DummyDestroyProcess : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    
    public void DestroyDummy()
    {
        EffectObject effect = PoolingManager.Instance.Pop(PoolingType.PlayerAppearVFX) as EffectObject;
        effect.Initialize(_targetObject.transform.position);
        Destroy(_targetObject);    
    }
        
}