using System;
using DG.Tweening;
using QuestManage;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestWindowUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private QuestListSO _quest;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _difficultyText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _icon;
    private bool _isAccept;
    private Image _panelImage;

    private void Awake()
    {
        _panelImage = GetComponent<Image>();
    }

    private void OnValidate()
    {
        if (_quest == null) return;
        SetQuest(_quest);
    }


    /**
     * 퀘스트 데이터를 설정
     */
    public void SetQuest(QuestListSO quest)
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
        return new QuestData(_quest.id, 100, _quest.difficulty);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isAccept) return;
        _isAccept = true;
        Debug.Log("Accept Quest");
        if (_quest == null) Debug.LogError("Quest is null");
        _panelImage.color = Color.green;
        (transform as RectTransform).DOAnchorPosX(1000f, 0.5f).SetEase(Ease.InOutBack)
            .OnComplete(() => transform.SetParent(null));
        QuestManager.Instance.AcceptQuest(_quest, GetQuestData());
    }
}
