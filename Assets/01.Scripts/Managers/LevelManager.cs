using System;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private ExpGaugePanel _expPanel;
    [SerializeField] private int _currentExp;
    public int MaxExp => CalcMaxExp(_currentLevel);
    /**
     * <summary>
     * 적응력 수치에 해당한다.
     * </summary>
     */
    private int _currentLevel;
    public int CurrentLevel => _currentLevel;
    public int CurrentExp => _currentExp;
    
    private int _gainExp;
    public event Action OnExpGainEvent;
    public event Action OnLevelUpEvent;

    private Player _player;
    
    protected override void Awake()
    {
        //base.Awake();
        OnExpGainEvent += CheckLevelUp;
        OnLevelUpEvent += HandleLevelUp;

    }

    private void Start()
    {
        _player = PlayerManager.Instance.player;
        _expPanel.Refresh(_currentLevel, _currentExp, MaxExp);

    }

    public void ApplyExp(int amount)
    {
        _currentExp += amount + amount;
        _expPanel.Refresh(_currentLevel, _currentExp, MaxExp);

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
        //return (int)(Mathf.Pow(3.5f, (float)level / 6  -0.2f) * 6 - 1.8f);
        return 50 * level;
    }

    private void HandleLevelUp()
    {
        UIManager.Instance.Open(WindowEnum.EffectSelect);
    }

    public void SetLevelExp(int level, int exp) {
        _currentExp = exp;
        _currentLevel = level;
        _expPanel.Refresh(_currentLevel, _currentExp, MaxExp);
    }
}
