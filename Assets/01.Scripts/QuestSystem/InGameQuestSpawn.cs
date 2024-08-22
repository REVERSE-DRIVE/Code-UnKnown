using System;
using QuestManage;
using UnityEngine;

public class InGameQuestSpawn : MonoBehaviour
{
    [SerializeField] private QuestItem _questItemPrefab;
    [SerializeField] private Transform _questItemParent;

    private void Start()
    {
        SpawnQuestItem();
    }

    public void Open()
    {
        
    }

    public void Close()
    {
        
    }

    private void SpawnQuestItem()
    {
        var quests = QuestObserver.Instance.currentQuestDatas;

        foreach (var quest in quests)
        {
            var questItem = Instantiate(_questItemPrefab, _questItemParent);
            questItem.SetQuestItem(quest);
        }
    }
}