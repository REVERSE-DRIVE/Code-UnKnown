using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class CoreButton : MonoBehaviour
{
    [Header("UI Setting")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _upgradeButton;
    private void Awake()
    {
        _upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Upgrade()
    {
    }
}