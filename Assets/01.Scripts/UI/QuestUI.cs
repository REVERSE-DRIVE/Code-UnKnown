using DG.Tweening;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    private RectTransform _questWindowTransform;

    private void Awake()
    {
        _questWindowTransform = transform as RectTransform;
    }
    [ContextMenu("Open Quest Window")]
    public void OpenQuestWindow()
    {
        _questWindowTransform.DOAnchorPos(Vector2.zero, 0.5f);
    }
    
    [ContextMenu("Close Quest Window")]
    public void CloseQuestWindow()
    {
        _questWindowTransform.DOAnchorPos(new Vector2(0, -1000), 0.5f);
    }
}