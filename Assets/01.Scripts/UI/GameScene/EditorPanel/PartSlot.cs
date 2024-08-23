using System;
using DG.Tweening;
using PlayerPartsManage;
using UnityEngine;
using UnityEngine.UI;

public abstract class PartSlot : MonoBehaviour
{
    public event Action<PlayerPartDataSO> OnSelectEvent; 
    [SerializeField] protected Image _selectDisplay;
    protected PlayerPartDataSO _currentPart;
    protected Button _button;
    
    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandlePartSelect);
    }

    protected void HandlePartSelect()
    {
        OnSelectEvent?.Invoke(_currentPart);
        _selectDisplay.DOFade(1f, 0.2f);
    }

    public void DisableSelect()
    {
        _selectDisplay.DOFade(0f, 0.2f);

    }

    public abstract void RefreshImage();



}