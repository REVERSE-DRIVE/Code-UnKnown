using QuestManage;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestWindowUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private QuestSO _quest;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _difficultyText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _icon;
    private bool _isAccept;
    
    private void OnValidate()
    {
        if (_quest == null) return;
        SetQuest(_quest);
    }


    /**
     * 퀘스트 데이터를 설정
     */
    public void SetQuest(QuestSO quest)
    {
        _quest = quest;
        _titleText.text = quest.title;
        _difficultyText.text = $"난이도: {quest.difficulty}";
        _descriptionText.text = quest.description;
        _icon.sprite = quest.icon;
    }
    
    /**
     * 퀘스트 데이터를 반환합니다.
     */
    public QuestData GetQuestData()
    {
        return new QuestData(_quest.id, _quest.goalValue);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isAccept) return;
        _isAccept = true;
        QuestManager.Instance.AcceptQuest(GetQuestData());
    }
}
