using System;
using DG.Tweening;
using PlayerPartsManage;
using UnityEngine;
using UnityEngine.UI;

public class PartSlot : MonoBehaviour
{
    public event Action<PlayerPartDataSO> OnSelectEvent; 
    [SerializeField] private Image _selectPartImage;
    [SerializeField] private Image _selectDisplay;
    private PlayerPartDataSO _currentPart;
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandlePartSelect);
    }

    private void HandlePartSelect()
    {
        OnSelectEvent?.Invoke(_currentPart);
        _selectDisplay.DOFade(1f, 0.2f);
    }

    public void DisableSelect()
    {
        _selectDisplay.DOFade(0f, 0.2f);

    }

    public void Initialize(PlayerPartDataSO part)
    {
        // 정보를 가지고있는 SO가 이게 아니라면 바꿔야될수도 있음
        _currentPart = part;
    }
    
    
}