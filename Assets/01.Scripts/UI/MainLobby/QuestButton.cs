using DG.Tweening;
using QuestManage;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    [SerializeField] private RectTransform _questPanel;
    [SerializeField] private float _endValue;
    [SerializeField] private float _questPanelMoveDuration;
    [SerializeField] private Ease _questPanelMoveEase;
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        QuestManager.Instance.SpawnQuestWindow();
    }
}