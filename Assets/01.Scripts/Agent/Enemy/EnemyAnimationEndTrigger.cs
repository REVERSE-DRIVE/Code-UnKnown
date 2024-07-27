using EnemyManage;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEndTrigger : MonoBehaviour
{
    [SerializeField] private Enemy _agent;

    public UnityEvent OnAnimationEvent;
    
    private void AnimationEnd()
    {
        _agent.AnimationEndTrigger();
    }

    private void AnimationEvent()
    {
        OnAnimationEvent?.Invoke();
    }
}