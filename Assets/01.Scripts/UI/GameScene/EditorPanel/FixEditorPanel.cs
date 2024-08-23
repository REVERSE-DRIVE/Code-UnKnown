using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FixEditorPanel : MonoBehaviour, IWindowPanel
{
    [Header("fix Setting")]
    [SerializeField] private int _requireResource;
    [SerializeField] private int _healAmount;
    [Header("Others")] 
    [SerializeField] private Button _fixBtn;
    [SerializeField] private Image _gaugeFill;
    [SerializeField] private Image _gaugeSubFill;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _requireResourceText;
    [SerializeField] private float _openDuration = 0.1f;
    [SerializeField] private float _gaugeFillDuration = 0.2f;

    private bool IsEnough => ResourceManager.Instance.ResourceAmount < _requireResource;
    private Vector3 _defaultScale = Vector3.one;
    private Player _player;
    private RectTransform _rectTrm;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        
        _canvasGroup = GetComponent<CanvasGroup>();
        _fixBtn.onClick.AddListener(HandleFixPlayer);
    }

    private void Start()
    {
        _player = PlayerManager.Instance.player;
    }

    public void Open()
    {
        SetCanvas(true);
        Sequence seq = DOTween.Sequence();
        _rectTrm.localScale = Vector3.zero;
        ResetGauge();
        if (IsEnough)
            _requireResourceText.color = Color.white;
        else 
            _requireResourceText.color = Color.red;
        _requireResourceText.text = _requireResource.ToString();
        
        seq.SetUpdate(true);
        seq.Append(_rectTrm.DOScaleX(_defaultScale.x, _openDuration));
        seq.Append(_rectTrm.DOScaleY(_defaultScale.y, _openDuration));
        seq.AppendCallback(RefreshGauge);
    }

    private void RefreshGauge()
    {
        ResetGauge();
        int hp = _player.HealthCompo.CurrentHealth;
        int maxHp = _player.HealthCompo.maxHealth;
        _healthText.text = $"{hp} / {maxHp}";

        _gaugeSubFill.DOFillAmount((hp + _healAmount) / (float)maxHp, _gaugeFillDuration);
        _gaugeFill.DOFillAmount(hp / (float)maxHp, _gaugeFillDuration);
    }

    private void ResetGauge()
    {
        _gaugeSubFill.fillAmount = 0f;
        _gaugeFill.fillAmount = 0f;
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

    private void HandleFixPlayer()
    {
        if (IsEnough) return;
        
        ResourceManager.Instance.UseResource(_requireResource);
        _player.HealthCompo.RestoreHealth(_healAmount);
        RefreshGauge();
    }
}
