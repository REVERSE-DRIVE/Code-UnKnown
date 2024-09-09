using UnityEngine;

public class PlayerHitEffectFeedback : Feedback
{
    [SerializeField] private float _shakePower = 30f;
    [SerializeField] private float _shakeDuration = 0.2f;
    
    public override void CreateFeedback()
    {
        VolumeEffectManager.Instance.SetChromaticAb(1, 0.1f, 0.05f);
        CameraManager.Instance.Shake(_shakePower, _shakeDuration);
    }

    public override void FinishFeedback()
    {
        
    }
}