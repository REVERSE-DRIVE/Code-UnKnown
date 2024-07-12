using System;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ExpGaugePanel _expPanel;
    [SerializeField] private int _currentExp;
    public int MaxExp => CalcMaxExp(_currentLevel);
    private int _currentLevel;
    public int CurrentLevel => _currentLevel;
    private int _gainExp;
    public event Action OnExpGainEvent;
    public event Action OnLevelUpEvent;
    
    private void Awake()
    {
        OnExpGainEvent += CheckLevelUp;
    }

    public void ApplyExp(int amount)
    {
        _currentExp += amount;
        _expPanel.Refresh(_currentExp, MaxExp);

        OnExpGainEvent?.Invoke();
    }

    private void CheckLevelUp()
    {
        if (_currentExp >= MaxExp)
        {
            _currentExp -= MaxExp;
            _currentLevel++;
            OnLevelUpEvent?.Invoke();
            
        }
        
    }

    public int CalcMaxExp(int level)
    {
        // 수식을 만들어 대입해야한다
        return 0;
    }
    
}
