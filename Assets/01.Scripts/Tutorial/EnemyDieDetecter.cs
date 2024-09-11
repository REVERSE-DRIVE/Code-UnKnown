using UnityEngine;
using UnityEngine.Events;

public class EnemyDieDetecter : MonoBehaviour
{
    public UnityEvent OnClearEvent;
    [SerializeField] private Health[] _targets;
    private int _targetAmount = 0;
    private int _currentProgress = 0;
    private void Awake()
    {
        _targetAmount = _targets.Length;
        for (int i = 0; i < _targetAmount; i++)
        {
            _targets[i].OnDieEvent.AddListener(HandleKillEvent);
        }
    }

    private void HandleKillEvent()
    {
        _currentProgress++;
        if (_currentProgress >= _targetAmount)
        {
            Clear();
        }
    }
    
    public void Clear()
    {
        OnClearEvent?.Invoke();
    }

}