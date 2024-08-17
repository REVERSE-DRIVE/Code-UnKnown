using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class CoreButton : MonoBehaviour
{
    [Header("Stat Setting")]
    [SerializeField] private StatIncUpEffectSO _stat;
    [SerializeField] private StatType _type;
    [SerializeField] private int[] _statIncrease;
    [Header("UI Setting")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;
    [field:SerializeField] private Button _upgradeButton { get; set; }
    private int _level = 1;
    
    private void Awake()
    {
        _upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Upgrade()
    {
        if (_stat.CanUpgradeEffect())
        {
            _stat.increaseValue = _statIncrease[_level];
            _stat.UseEffect();
            _level++;
            _text.text = _stat.increaseValue.ToString();
        }
    }
}