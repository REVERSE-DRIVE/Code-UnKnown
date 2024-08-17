using System;
using UnityEngine;

public class AttackCountSkill : AttackTriggerSkill
{
    [SerializeField] protected int _targetCount;
    public event Action onAttackCountEvent;
    
    protected override void HandlePlayerAttackEvent()
    {
        base.HandlePlayerAttackEvent();
        if(_attackCount >= _targetCount)
            onAttackCountEvent?.Invoke();
    }
}

