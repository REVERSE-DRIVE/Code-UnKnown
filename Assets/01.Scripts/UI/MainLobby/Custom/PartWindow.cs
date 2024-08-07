using DG.Tweening;
using PlayerPartsManage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartWindow : MonoBehaviour, IWindowPanel
{
    [SerializeField] private RectTransform _partItemParent;
    [SerializeField] private RectTransform _partParent;
    [SerializeField] private TextMeshProUGUI _partNameText;
    [SerializeField] private TextMeshProUGUI _partDescriptionText;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _amountButton;
    
    private RectTransform _rectTransform;
    private CustomItem _customItem;
    
    private void Awake()
    {
        _rectTransform = transform as RectTransform;
        _closeButton.onClick.AddListener(Close);
        _amountButton.onClick.AddListener(Amount);
    }

    private void Amount()
    {
        if (_customItem == null) return;
        switch (_customItem.PartData)
        {
            case PlayerLegPartDataSO legPartData:
                _customItem._customIcon.ChangeIconImage(false, legPartData.legPartSprites);
                PlayerPartManager.Instance.SetLegPart(legPartData);
                break;
            case PlayerBodyPartDataSO bodyPartData:
                _customItem._customIcon.ChangeIconImage(true, bodyPartData.bodyPartSprite);
                PlayerPartManager.Instance.SetBodyPart(bodyPartData);
                break;
        }
    }

    public void SetChild(CustomItem customItem)
    {
        var rect = customItem.transform as RectTransform;
        Open();
        customItem.transform.SetParent(_partItemParent);
        rect.localScale = Vector3.one;
        _customItem = customItem;
        SetPartInfo(customItem);
        rect.DOLocalMove(Vector3.zero, 0.5f)
            .OnComplete(() =>
            {
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.anchoredPosition = Vector2.zero;
            });
    }
    
    private void SetPartInfo(CustomItem partData)
    {
        _partNameText.text = partData.PartData.partName;
        _partDescriptionText.text = partData.PartData.description;
    }

    public void Open()
    {
        _rectTransform.DOScaleX(1f, 0.5f);
    }

    public void Close()
    {
        if (_customItem != null)
            _customItem.transform.SetParent(_partParent);
        _rectTransform.DOScaleX(0f, 0.5f);
    }
}