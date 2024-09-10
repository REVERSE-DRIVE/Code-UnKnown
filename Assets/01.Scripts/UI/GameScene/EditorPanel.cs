using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorPanel : UIPanel
{
    [SerializeField] private Button _partBtn;
    [SerializeField] private Button _fixBtn;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private PartEditorPanel _partEditorPanel;
    [SerializeField] private FixEditorPanel _fixEditorPanel;
    [SerializeField] private TextMeshProUGUI _resourceAmountText;

    protected override void Awake()
    {
        base.Awake();
        _partBtn.onClick.AddListener(_fixEditorPanel.Close);
        _partBtn.onClick.AddListener(_partEditorPanel.Open);

        _fixBtn.onClick.AddListener(_partEditorPanel.Close);
        _fixBtn.onClick.AddListener(_fixEditorPanel.Open);
        _exitBtn.onClick.AddListener(HandleExit);
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceChnaged += HandleRefreshResourceText;
    }

    public override void Open()
    {
        base.Open();
    }

    private void HandleExit()
    {
        Close();
    }

    private void HandleRefreshResourceText(int amount)
    {
        _resourceAmountText.text = amount.ToString();
    }
    
    

   
}
