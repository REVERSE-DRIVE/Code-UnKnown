using System;
using QuestManage;
using UnityEngine;

public class InGameQuestManager : MonoBehaviour
{
    [SerializeField] private QuestItem _questItemPrefab;
    [SerializeField] private Transform _questItemParent;
    
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        SetVisible(false);
    }

    private void Start()
    {
        SpawnQuestItem();
    }

    public void Open()
    {
        SetVisible(true);
    }

    public void Close()
    {
        SetVisible(false);
    }

    private void SpawnQuestItem()
    {
        var quests = QuestObserver.Instance.currentQuestDatas;
        if (quests == null) return;

        foreach (var quest in quests)
        {
            var questItem = Instantiate(_questItemPrefab, _questItemParent);
            questItem.SetQuestItem(quest);
        }
    }
    
    private void SetVisible(bool visible)
    {
        _canvasGroup.alpha = visible ? 1 : 0;
        _canvasGroup.blocksRaycasts = visible;
        _canvasGroup.interactable = visible;
    }
}