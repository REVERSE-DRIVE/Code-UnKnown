using System;
using UnityEngine;

public class EffectSelectPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    // 중첩 레벨업을 대비하여 이를 저장
    private int _levelUpAmount;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
    }


    public void HandleLevelUpEvent()
    {
        // 레벨업 했을때
        _levelUpAmount++;
        
    }


    // 효과를 선택했을때
    public void HandleSelected()
    {
        _levelUpAmount--;
        

    }

}