using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private Button _startButton;
    [SerializeField] private float _duration;
    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _startButton.onClick.AddListener(HandleStartButtonEvent);
    }

    private void HandleStartButtonEvent()
    {
        SceneManager.LoadScene("MainLobbyScene");
    }

    public void Open()
    {
        CanvasActive(true);
    }

    public void Close()
    {
        CanvasActive(false);
    }

    private void CanvasActive(bool value)
    {
        _canvasGroup.DOFade(value ? 1f : 0f, _duration);
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;   
    }
}
