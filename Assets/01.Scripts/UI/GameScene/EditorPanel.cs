using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EditorPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private float _activeDuration;
    [SerializeField] private Button _partBtn;
    [SerializeField] private Button _fixBtn;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private PartEditorPanel _partEditorPanel;
    [SerializeField] private FixEditorPanel _fixEditorPanel;
        
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _partBtn.onClick.AddListener(_fixEditorPanel.Close);
        _partBtn.onClick.AddListener(_partEditorPanel.Open);

        _fixBtn.onClick.AddListener(_partEditorPanel.Close);
        _fixBtn.onClick.AddListener(_fixEditorPanel.Open);
        _exitBtn.onClick.AddListener(HandleExit);
    }

    private void HandleExit()
    {
        Close();
    }

    public void Open()
    {
        SetCanvasGroup(true);
        
    }

    public void Close()
    {
        SetCanvasGroup(false);
    }

    private void SetCanvasGroup(bool value)
    {
        _canvasGroup.DOFade(value ? 1f : 0f, _activeDuration);
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }
}
