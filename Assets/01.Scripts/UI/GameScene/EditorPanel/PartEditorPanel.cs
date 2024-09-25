using System;
using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using PlayerPartsManage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartEditorPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private int _requireResource;
    [SerializeField] private Image _detailPanelImage;
    [SerializeField] private TextMeshProUGUI _partNameText;
    [SerializeField] private TextMeshProUGUI _partDescriptionText;
    [SerializeField] private GameObject _notEnoughPanel;
    [SerializeField] private Button _saveBtn;
    
    [SerializeField] private BodySlot _bodySlot;
    [SerializeField] private LegSlot _legSlot;

    private Player _player;
    private CanvasGroup _canvasGroup;
    private bool IsEnough => ResourceManager.Instance.ResourceAmount >= _requireResource;
    private bool _isEnough;

    private float _currentTime = 0;
    private float _blinkTerm = 0.7f;
    private PlayerPartDataSO _currentPartData;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _saveBtn.onClick.AddListener(Save);
    }

    private void Start()
    {
        _player = PlayerManager.Instance.player;
        _bodySlot.OnSelectEvent += RefreshInfo;
        _legSlot.OnSelectEvent += RefreshInfo;
    }

    private void Update()
    {
        _isEnough = IsEnough;
        if (!_canvasGroup.interactable) return;
        if (_isEnough)
        {
            _notEnoughPanel.SetActive(false);
            return;

        }
        _currentTime += Time.deltaTime;
        if (_currentTime > _blinkTerm)
        {
            _currentTime = 0;
            _notEnoughPanel.SetActive(!_notEnoughPanel.activeInHierarchy);
        }
    }

    public void RefreshInfo(PlayerPartDataSO dataSO)
    {
        _currentPartData = dataSO;
        _partNameText.text = dataSO.partName;
        _partDescriptionText.text = dataSO.description;
    }

    public void Open()
    {
        _detailPanelImage.fillAmount = 0f;
        _detailPanelImage.DOFillAmount(1f, 0.3f);
        _bodySlot.Initialize(_player.PlayerPartController.CurrentBodyPart);
        _legSlot.Initialize(_player.PlayerPartController.CurrentLegPart);
        _isEnough = IsEnough;
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

    private void Save()
    {
        if (!_isEnough) return;
        PlayerPartManager.Instance.AddPartData(_currentPartData);
        ResourceManager.Instance.UseResource(_requireResource);
        _partDescriptionText.text = "획득 완료";
    }
}
