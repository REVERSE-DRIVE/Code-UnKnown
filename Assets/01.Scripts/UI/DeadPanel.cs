using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DeadPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _deadText;

    private void Start()
    {
        _backButton.onClick.AddListener(HandleLobbyMove);
    }

    private void HandleLobbyMove()
    {
        LoadManager.Instance.StartLoad("MainLobbyScene");
    }


    public void Open()
    {
        SetUI();
    }

    private void SetUI()
    {
        _deadText.text = "You are dead!";
    }

    public void Close()
    {
        
    }
}
