using UnityEngine;
using System;
using QuestManage;
using TMPro;
using UnityEngine.UI;

public class InGameQuestPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private TextMeshProUGUI _questTitle;
    [SerializeField] private TextMeshProUGUI _questDescription;
    [SerializeField] private TextMeshProUGUI _goalText;
    [SerializeField] private RectTransform _questPanel;
    [SerializeField] private Image _icon;
    
    public QuestItem _questItem;
    public QuestSO _quest;
    public QuestData _questData;
    
    private CanvasGroup _canvasGroup;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void SetQuestUI(QuestData quest)
    {
        _quest = QuestManager.Instance.FindQuest(quest.id, quest.difficulty);
        _questData = quest;
        _questTitle.text = _quest.title;
        _questDescription.text = _quest.description;
        _goalText.text = quest.progressValue + " / " + _quest.goalValue;
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
        SetQuestUI(_questData);
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }
}