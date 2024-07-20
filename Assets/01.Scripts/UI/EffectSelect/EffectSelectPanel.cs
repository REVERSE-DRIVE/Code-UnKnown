using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class EffectSelectPanel : MonoBehaviour, IWindowPanel
{
    [SerializeField] private EffectSelectOption[] OptionSlots;
    [SerializeField] private PowerUpListSO _powerUpList;
    
    private CanvasGroup _canvasGroup;
    // 중첩 레벨업을 대비하여 이를 저장
    private int _levelUpAmount;

    private bool _isActive = false;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
    }
    
    private void SetUpPowerUpCards()
    {
        PowerUpSO[] arr = _powerUpList.list.Where(x => x.CheckCanUpgrade()).ToArray();

        if (arr.Length < 3)
        {
            Debug.LogError("Error!, Must Have 3 Item at Least");
        }

        for (int i = 0; i < 3; i++)
        {
            int index = UnityEngine.Random.Range(0, arr.Length - i);
            OptionSlots[i].SetCardData(arr[index]);
            arr[index] = arr[arr.Length - 1 - i];
        }
        
    }

    public void Open()
    {
        _levelUpAmount++;
        if (_levelUpAmount > 1) return;
        
        SetUpPowerUpCards();
        _canvasGroup.DOFade(1, 0.3f).SetUpdate(true).OnComplete(() => SetInteract(true));
    }

    public void Close()
    {
        _levelUpAmount--;
        if (_levelUpAmount > 0)
        {
            SetUpPowerUpCards();
            return;
        }
        SetInteract(false);
        _canvasGroup.DOFade(0, 0.3f).SetUpdate(false);
    }

    private void SetInteract(bool value)
    {
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
        
    }
}