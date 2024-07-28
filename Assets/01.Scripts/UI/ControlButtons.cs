using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{
    [Header("Button Setting")] 
    public HoldButton actionButton;
    public HoldButton skillButton;
    
    [SerializeField] private Sprite[] _buttonSprites;
    private Image _buttonImage;
    private bool _isInteractMode;

    public event Action OnInteractEvent;
    public event Action OnAttackEvent;
    public event Action OnHoldAttackEvent;
    
    public event Action OnSkillEvent;

    private void Awake()
    {
        _buttonImage = actionButton.buttonImage;
        
        actionButton.OnTapEvent += HandleActionButtonClick;
        actionButton.OnHoldEvent += HandleActionButtonHold;

        skillButton.OnTapEvent += HandleSkillButtonClick;
    }


    private void Start()
    {
    }

    #region Interaction Switching

    private void SetInteractMode(bool value)
    {
        _buttonImage.sprite = value ? _buttonSprites[1] : _buttonSprites[0];
    }

    public void HandleInteractMode()
    {
        if(_isInteractMode) return;
        SetInteractMode(true);
        _isInteractMode = true;

    }

    public void HandleAttackMode()
    {
        if(!_isInteractMode) return;
        SetInteractMode(false);
        _isInteractMode = false;
    }

    #endregion



    private void HandleActionButtonClick()
    {
        if (_isInteractMode)
            OnInteractEvent?.Invoke();
        else
            OnAttackEvent?.Invoke();
    }

    private void HandleActionButtonHold()
    {
        if (_isInteractMode) return;
        // 홀드스킬 관련 구현해야한다
    }
    
    
    private void HandleSkillButtonClick()
    {
        OnSkillEvent?.Invoke();
    }
}
