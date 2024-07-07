using ObjectManage;
using ObjectPooling;
using UnityEngine;

public class PlayHitImpactFeedback : Feedback
{
    [SerializeField] private float _playTime = 1.5f;
    [SerializeField] private PoolingType _hitVFXType;
    public override void CreateFeedback()
    {
        var effect = PoolingManager.Instance.Pop(_hitVFXType) as EffectObject;
        //ActionData actionData = _owner.HealthCompo.actionData;

        //Quaternion rot = Quaternion.LookRotation(actionData.hitNormal * -1);
        //effect.transform.SetPositionAndRotation(actionData.hitPoint, rot);
        effect.Initialize(_owner.transform.position);
        effect.Play();
        
    }

    public override void FinishFeedback()
    {
        
    }
}