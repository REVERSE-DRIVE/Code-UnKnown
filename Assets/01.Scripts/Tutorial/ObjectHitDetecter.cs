using UnityEngine;

public class ObjectHitDetecter : ConditionObject
{
    [SerializeField] private int _targetCount = 5;
    private int _currentCount = 0;
    private bool _isActive = false;
    
    public void Cont()
    {
        _currentCount++;

        if(_currentCount >= _targetCount && !_isActive)
        {
            _isActive = true;
            OnActiveEvent?.Invoke();
        }
    }
}