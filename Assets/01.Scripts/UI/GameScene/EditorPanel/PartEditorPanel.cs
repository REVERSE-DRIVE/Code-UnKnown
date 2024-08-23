using System;
using DG.Tweening;
using PlayerPartsManage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartEditorPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private Image _detailPanelImage;
    [SerializeField] private TextMeshProUGUI _partNameText;
    [SerializeField] private TextMeshProUGUI _partDescriptionText;

    [SerializeField] private BodySlot _bodySlot;
    [SerializeField] private LegSlot _legSlot;

    private Player _player;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _player = PlayerManager.Instance.player;
        _bodySlot.OnSelectEvent += RefreshInfo;
        _legSlot.OnSelectEvent += RefreshInfo;
    }

    public void RefreshInfo(PlayerPartDataSO dataSO)
    {
        _partNameText.text = dataSO.partName;
        _partDescriptionText.text = dataSO.description;
    }

    public void Open()
    {
        _detailPanelImage.fillAmount = 0f;
        _detailPanelImage.DOFillAmount(1f, 0.3f);
        _bodySlot.Initialize(_player.PlayerPartController.CurrentBodyPart);
        _legSlot.Initialize(_player.PlayerPartController.CurrentLegPart);
        
        SetCanvas(true);
    }

    public void Close()
    {
        SetCanvas(false);
    }
    
    private void SetCanvas(bool value)
    {
        _canvasGroup.alpha = value ? 1f : 0f;
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}
