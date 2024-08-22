using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using QuestManage;
using UnityEngine;
using TMPro;

public class QuestItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private InGameQuestPanel _inGameQuestPanel;
    [SerializeField] private QuestSO _quest;

    public void SetQuestItem(QuestSO quest)
    {
        _quest = quest;
        _icon.sprite = quest.icon;
        _titleText.text = quest.title;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _inGameQuestPanel._questItem = this;
        _inGameQuestPanel._quest = _quest;
        _inGameQuestPanel.Open();
    }
}
