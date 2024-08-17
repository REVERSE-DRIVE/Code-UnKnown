using System;
using PlayerPartsManage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CustomItem : MonoBehaviour
{
    [SerializeField] private Image[] _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    public CustomIcon _customIcon;
    [field:SerializeField] public PlayerPartDataSO PartData { get; set; }
    
    protected Button _button;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void SetUI(string name, params Sprite[] icon)
    {
        _nameText.text = name;
        for (int i = 0; i < _icon.Length; i++)
        {
            _icon[i].sprite = icon[i];
        }
    }

    protected abstract void OnClick();
}