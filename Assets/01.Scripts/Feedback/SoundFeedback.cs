using ObjectPooling;
using SoundManage;
using UnityEngine;

public class SoundFeedback : Feedback
{
    [SerializeField] private SoundSO _soundData;

    public override void CreateFeedback()
    {
        SoundObject sound = PoolingManager.Instance.Pop(PoolingType.SoundObject) as SoundObject;
        sound.PlaySound(_soundData);
    }

    public override void FinishFeedback()
    {
    }
}