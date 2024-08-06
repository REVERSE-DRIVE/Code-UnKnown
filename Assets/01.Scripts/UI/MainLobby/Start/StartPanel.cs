﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : WindowUI
{
    [Header("UI Setting")]
    [SerializeField] private Image _bodyIcon;
    [SerializeField] private Image[] _legIcon;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _startButton;
    [SerializeField] private FadeInOut _fadeInOut;
    
    private PlayerPartManager _playerPartManager;

    protected override void Awake()
    {
        base.Awake();
        _playerPartManager = PlayerPartManager.Instance;
        _startButton.onClick.AddListener(StartGame);
        OnFocus += SetUI;
    }

    private void StartGame()
    {
        _fadeInOut.Fade(0.5f, 1f, 
            () => SceneManager.LoadScene("GameScene"));
    }

    private void OnDestroy()
    {
        OnFocus -= SetUI;
    }

    private void SetUI()
    {
        _bodyIcon.sprite = _playerPartManager.BodyPart.bodyPartSprite;
        
        for (int i = 0; i < _legIcon.Length; i++)
        {
            _legIcon[i].sprite = _playerPartManager.LegPart.legPartSprites[i];
        }
        
        _descriptionText.text 
            = $"현재 장착한 파츠는 \n{_playerPartManager.BodyPart.partName}, {_playerPartManager.LegPart.partName}입니다.\n계속하시겠습니까?";
    }
}