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
    [SerializeField] private QuestListSO _quest;
    
    private QuestData _questData;

    private void Start()
    {
        
    }

    public void SetQuestItem(QuestSO quest)
    {
        // _quest = quest;
        // _icon.sprite = quest.icon;
        // _titleText.text = quest.title;
    }
    
    public void SetQuestItem(QuestData questData)
    {
        _questData = questData;
        _quest = QuestManager.Instance.FindQuestListSO(questData.id, questData.difficulty);
        _icon.sprite = _quest.icon;
        _titleText.text = _quest.title;
        _inGameQuestPanel = FindObjectOfType<InGameQuestPanel>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        _inGameQuestPanel._questItem = this;
        _inGameQuestPanel._quest = _quest;
        _inGameQuestPanel._questData = _questData;
        _inGameQuestPanel.Open();
    }
}
