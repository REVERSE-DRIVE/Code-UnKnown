using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossCutScenePanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private TextMeshProUGUI _bossNameText;
    [SerializeField] private Image _bossImage;
    [SerializeField] private RectTransform _bossImageTrm;

    [Header("Setting Values")] 
    [SerializeField] private float _cutSceneTerm = 1f;
    [SerializeField] private float _activeDuration;

    [SerializeField] private float _bossImageDefaultPosition;
    [SerializeField] private float _bossImageActivePosition;
    [SerializeField] private float _bossImageActiveDuration;    
    
    private BossInfoSO _currentBossInfo;
    private RectTransform _rectTrm;
    private Sequence _seq;
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        _rectTrm = transform as RectTransform;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(BossInfoSO info)
    {
        _currentBossInfo = info;
        _bossImage.sprite = info.bossIcon;
        _bossNameText.text = info.bossName;
    }

    public void ShowCutScene()
    {
        _seq = DOTween.Sequence();
        _seq.SetUpdate(true);
        Open();
        _seq.AppendInterval(_cutSceneTerm);
        Close();
    }

    
    [ContextMenu("DebugOpen")]
    public void Open()
    {
        Time.timeScale = 0f;
        transform.localScale = new Vector3(1, 0f);
        _canvasGroup.alpha = 0f;
        _bossImageTrm.anchoredPosition = new Vector2(_bossImageDefaultPosition, 0);
        _seq.Append(transform.DOScaleY(1f, _activeDuration));
        _seq.Join(_canvasGroup.DOFade(1, _activeDuration).SetEase(Ease.OutExpo));
        _seq.Append(_bossImageTrm.DOAnchorPosX(_bossImageActivePosition, _bossImageActiveDuration));
        
    }
    

    public void Close()
    {
        _seq.Append(transform.DOScaleY(0f, _activeDuration));
        _seq.Join(_canvasGroup.DOFade(0f, _activeDuration).SetEase(Ease.InExpo));
        _seq.AppendCallback(() => Time.timeScale = 1f);
    }
    
    
}
