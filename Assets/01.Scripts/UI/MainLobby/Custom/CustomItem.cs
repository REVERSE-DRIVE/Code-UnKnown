using System;
using PlayerPartsManage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CustomItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] protected CustomIcon _customIcon;
    
    protected Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _customIcon = FindObjectOfType<CustomIcon>();
        _button.onClick.AddListener(OnClick);
    }

    protected abstract void Start();

    protected void SetUI(Sprite icon, string name)
    {
        _icon.sprite = icon;
        _nameText.text = name;
    }

    protected abstract void OnClick();
}