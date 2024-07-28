using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public enum ButtonType
{
    Attack,
    Skill
}

public class HoldButton : MonoBehaviour
{
    private MainInput _mainInput;
    [SerializeField] private bool _isHold;
    [SerializeField] private ButtonType _buttonType;
    public event Action OnTapEvent;
    public event Action OnHoldEvent;
    public Image buttonImage;
    
    
    private void Awake()
    {
        _mainInput = new MainInput();
        buttonImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        switch (_buttonType)
        {
            case ButtonType.Attack:
                _mainInput.Player.Attack.performed += Performed;
                _mainInput.Player.Attack.canceled += Canceled;
                break;
            case ButtonType.Skill:
                _mainInput.Player.Skill.performed += Performed;
                _mainInput.Player.Skill.canceled += Canceled;
                break;
        }
       
        _mainInput.Player.Enable();
    }

    private void OnDisable()
    {
        switch (_buttonType)
        {
            case ButtonType.Attack:
                _mainInput.Player.Attack.performed -= Performed;
                _mainInput.Player.Attack.canceled -= Canceled;
                break;
            case ButtonType.Skill:
                _mainInput.Player.Skill.performed -= Performed;
                _mainInput.Player.Skill.canceled -= Canceled;
                break;
        }
        _mainInput.Player.Disable();
    }


    private void Canceled(InputAction.CallbackContext context)
    {
        
    }

    private void Performed(InputAction.CallbackContext context)
    {
        
        if (context.interaction is HoldInteraction)
        {
            OnHoldEvent?.Invoke();
        }

        if (context.interaction is TapInteraction)
        {
            OnTapEvent?.Invoke();
        }
    }
}