using System;
using QuestManage;
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
    [SerializeField] private Button _clearButton;
    [SerializeField] private FadeInOut _fadeInOut;
    
    private PlayerPartManager _playerPartManager;
    private TextMeshProUGUI _startBtnText;

    protected override void Awake()
    {
        base.Awake();
        _playerPartManager = PlayerPartManager.Instance;
        _startButton.onClick.AddListener(StartGame);
        _clearButton.onClick.AddListener(ClearAndStartGame);
        OnFocus += SetUI;

        _startBtnText = _startButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void StartGame()
    {
        _fadeInOut.Fade(0.5f, 1f,
                () => LoadManager.Instance.StartLoad("GameScene"));
        
    }
    
    private void ClearAndStartGame()
    {
        GameManager.Instance.ClearInGameData();
        StartGame();
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
        QuestManager.Instance.CloseQuestWindow();

        // 데이터 불러옴
        bool lastSaved = GameManager.Instance.IsSavedInGameData();
        _startBtnText.text = lastSaved ? "이어서 하기" : "시작!";
        _clearButton.gameObject.SetActive(lastSaved);
    }
}