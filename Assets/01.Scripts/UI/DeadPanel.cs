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
    [SerializeField] private TextMeshProUGUI _liveTimeText;
    [SerializeField] private TextMeshProUGUI _resourceText;
    [SerializeField] private TextMeshProUGUI _partText;
    
    private CanvasGroup _canvasGroup;
    private bool _isShow;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _backButton.onClick.AddListener(HandleLobbyMove);
        _backButton.gameObject.SetActive(false);
    }

    private void HandleLobbyMove()
    {
        Time.timeScale = 1;
        GameManager.Instance.ClearInGameData(); // 죽었으니 초기화
        GameManager.Instance.ExitGame();
    }


    [ContextMenu("Open")]
    public void Open()
    {
        SetVisible(true);
        SetUI(InGameCounter.Instance.TimeCounter, 10, 5);
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
    }

    public void Close()
    {
        
    }
    
    private void SetVisible(bool visible)
    {
        _canvasGroup.alpha = visible ? 1 : 0;
        _canvasGroup.blocksRaycasts = visible;
        _canvasGroup.interactable = visible;
    }
}
