using System;
using UnityEngine;

public class AgentDieCheckDetecter : ConditionObject
{
    [SerializeField] private Agent[] _targetAgents;
    private int _amount;
    private int _currentDieAmount = 0;

    private void Start()
    {
        _amount = _targetAgents.Length;
        for (int i = 0; i < _amount; i++)
        {
            _targetAgents[i].HealthCompo.OnDieEvent.AddListener(HandleAgentDie);
        }
        
    }

    private void HandleAgentDie()
    {
        _currentDieAmount++;
        if (_currentDieAmount >= _amount)
        {
            OnActiveEvent?.Invoke();
        }
    }
}