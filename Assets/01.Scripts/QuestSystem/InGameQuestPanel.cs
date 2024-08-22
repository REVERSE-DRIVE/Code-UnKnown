using UnityEngine;
using System;
using QuestManage;
using TMPro;
using UnityEngine.UI;

public class InGameQuestPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private TextMeshProUGUI _questTitle;
    [SerializeField] private TextMeshProUGUI _questDescription;
    [SerializeField] private RectTransform _questPanel;
    [SerializeField] private Image _icon;
    
    public QuestItem _questItem;
    public QuestSO _quest;

    private void SetQuestUI(QuestSO quest)
    {
        _questTitle.text = quest.title;
        _questDescription.text = quest.description;
        _icon.sprite = quest.icon;
    }

    public void Open()
    {
        _questPanel.gameObject.SetActive(true);
        SetQuestUI(_quest);
    }

    public void Close()
    {
        _questPanel.gameObject.SetActive(false);
    }
}