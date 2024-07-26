using DG.Tweening;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    private RectTransform _questWindowTransform;

    private void Awake()
    {
        _questWindowTransform = transform as RectTransform;
    }
    
    /**
     * 퀘스트 UI 열기
     */
    public void OpenQuestWindow()
    {
        _questWindowTransform.DOAnchorPos(Vector2.zero, 0.5f);
    }
    
    /**
     * 퀘스트 UI 닫기
     */
    public void CloseQuestWindow()
    {
        _questWindowTransform.DOAnchorPos(new Vector2(1000, 0), 0.5f);
    }
}