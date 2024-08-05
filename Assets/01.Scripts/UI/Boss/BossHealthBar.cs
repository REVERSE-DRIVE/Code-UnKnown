using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour, IWindowPanel
{
    [SerializeField] private Image _gaugeFill;
    [SerializeField] private TextMeshProUGUI _binaryText;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _defaultYPos;
    [SerializeField] private float _activeYPos;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTrm;
    public BossInfoSO bossInfo;
    private Boss _boss;
    
    private static readonly string[] binaryTextArr =
    {
        "0101001001000100001010111011011001",
        "011111000110011111100110110110110111",
        "010110101010101101001010011110110",
        "AC00D011492E835A810AAAB72900BCDDE9",
        "1000000000000000001110011001001110"
    };

    private int _currentIndex = 0;
    private float _currentTime = 0;
    private float _countCoolTime = 0.2f;
    private bool _isChanging;
    private Color _gaugeColor;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTrm = transform as RectTransform;
        
    }

    public void Initialize(BossInfoSO info, Boss bossInstance)
    {
        bossInfo = info;
        _boss = bossInstance;
        _gaugeColor = info.personalColor;
        _boss.HealthCompo.OnHealthChangedValueEvent += HandleRefreshGauge;
        _boss.HealthCompo.OnDieEvent.AddListener(Close);
    }

    private void HandleRefreshGauge(int prevvalue, int newvalue, int max)
    {
        _gaugeFill.fillAmount = Mathf.Clamp01((float)newvalue / max);
        StartCoroutine(RefreshCoroutine());

    }

    private IEnumerator RefreshCoroutine()
    {
        _gaugeFill.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        _gaugeFill.color = _gaugeColor;
    }
    

    private void Update()
    {
        UpdateBinary(); 
    }

    private void UpdateBinary()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _countCoolTime)
        {
            _currentTime = 0;
            _currentIndex = (_currentIndex + 1) % binaryTextArr.Length;
            _binaryText.text = binaryTextArr[_currentIndex];
            
        }
    }

    public void Open()
    {
        _canvasGroup.DOFade(1f, _moveDuration);
        _rectTrm.DOAnchorPosY(_activeYPos, _moveDuration);
    }

    public void Close()
    {
        _canvasGroup.DOFade(0f, _moveDuration);
        _rectTrm.DOAnchorPosY(_defaultYPos, _moveDuration);
    }
}
