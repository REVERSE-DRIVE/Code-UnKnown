using System;
using DG.Tweening;
using PlayerPartsManage;
using UnityEngine;
using UnityEngine.UI;

public class CustomIcon : MonoBehaviour
{
    [SerializeField] private RectTransform _choicePanel;
    [SerializeField] private Image[] _icon;
    private Button _button;
    private PlayerBodyPartDataSO _bodySO;
    private PlayerLegPartDataSO _legSO;

    public PlayerBodyPartDataSO BodySO
    {
        get => _bodySO;
        set
        {
            _bodySO = value;
            _icon[0].sprite = _bodySO.bodyPartSprite;
        }
    }

    public PlayerLegPartDataSO LegSO
    {
        get => _legSO;
        set
        {
            _legSO = value;
            for (int i = 1; i < _icon.Length; i++)
            {
                _icon[i].sprite = _legSO.legPartSprites[i];
            }
        }
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClickAnimation);
    }

    private void ClickAnimation()
    {
        _choicePanel.gameObject.SetActive(true);
        _choicePanel.DOScaleY(1f, 0.5f).SetEase(Ease.OutBack);
    }
}