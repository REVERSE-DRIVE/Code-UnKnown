using System;
using System.Collections;
using DG.Tweening;
using QuestManage;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DeadPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _RevivalButton;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _liveTimeText;
    [SerializeField] private TextMeshProUGUI _resourceText;
    [SerializeField] private TextMeshProUGUI _partText;
    
    private CanvasGroup _canvasGroup;
    private bool _isShow;
    bool _revivalActive = true;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _backButton.onClick.AddListener(HandleLobbyMove);
        _backButton.gameObject.SetActive(false);

        _RevivalButton.onClick.AddListener(HandleRevivalPlayer);
        _RevivalButton.gameObject.SetActive(false);
    }

    private void HandleRevivalPlayer()
    {
        GoogleRewardManager.Instance.Show(() => {
            Close();
            PlayerManager.Instance.Revival();
        });
    }

    private void HandleLobbyMove()
    {
        Time.timeScale = 1;
        GameManager.Instance.ClearInGameData(); // 죽었으니 초기화
        GameManager.Instance.ExitGame();
    }


    public void Open()
    {
        SetVisible(true);
        SetUI(InGameCounter.Instance.TimeCounter, 10, 5);

        // 부활 이미 하면 비활함
        if (!PlayerManager.Instance.IsFirstSpawn)
            RevivalBtnHide();
    }
    
    public void SetTitle(string title)
    {
        _titleText.text = title;
    }

    public void RevivalBtnHide() {
        _revivalActive = false;
    }

    public void SetUI(float time, int resourceAmount, int partAmount)
    {
        if (_isShow) return;
        _liveTimeText.text = string.Empty;
        _resourceText.text = string.Empty;
        _partText.text = string.Empty;
        StartCoroutine(TextAnimation(time, resourceAmount, partAmount));
    }
    
    private IEnumerator TextAnimation(float time, int resourceAmount, int partAmount)
    {
        _isShow = true;
        Time.timeScale = 0;
        var liveTime = TimeSpan.FromSeconds(time);
        _liveTimeText.text = $"생존 시간: --:--";
        yield return new WaitForSecondsRealtime (0.5f);
        _resourceText.text = $"수집한 리소스: 0";
        yield return new WaitForSecondsRealtime (0.5f);
        _partText.text = $"수집한 파츠 개수: 0";
        
        yield return new WaitForSecondsRealtime (1f);
        _backButton.gameObject.SetActive(true);
        
        if (_revivalActive)
           _RevivalButton.gameObject.SetActive(true);
    }

    public void Close()
    {
        _isShow = false;
        Time.timeScale = 1;
        _backButton.gameObject.SetActive(false);
        _RevivalButton.gameObject.SetActive(false);
        SetVisible(false);

    }
    
    private void SetVisible(bool visible)
    {
        _canvasGroup.alpha = visible ? 1 : 0;
        _canvasGroup.blocksRaycasts = visible;
        _canvasGroup.interactable = visible;
    }
}
