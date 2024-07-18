using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour
{
    [Header("Button Setting")]
    public Button actionButton;
    public Button skillButton;
    
    [SerializeField] private Sprite[] _buttonSprites;
    private Image _buttonImage;
    private bool _isInteractMode;

    public event Action OnInteractEvent;
    public event Action OnAttackEvent;
    private MainInput _mainInput;
    
    public event Action OnSkillEvent;

    private void Awake()
    {
        _mainInput = new MainInput();
        _buttonImage = actionButton.GetComponent<Image>();
        actionButton.onClick.AddListener(HandleActionButtonClick);
    }

    private void Start()
    {
        _mainInput.Player.Attack.performed += Performed;
        _mainInput.Player.Attack.canceled += Canceled;
        _mainInput.Player.Attack.Enable();
    }

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


    private void Performed(InputAction.CallbackContext context)
    {
        if (context.time < 0.2)
        {
            Debug.Log("<color=blue>Attack");
            HandleActionButtonClick();
        }
        else if (context.time > 0.5)
        {
            // Hold
            Debug.Log("<color=blue>Hold Attack</color>");
        }
    }

    private void Canceled(InputAction.CallbackContext context)
    {
        Debug.Log("<color=blue>Canceled</color>");
    }
    
    private void HandleActionButtonClick()
    {
        if (_isInteractMode)
        {
            OnInteractEvent?.Invoke();

        }
        else
        {
            OnAttackEvent?.Invoke();
        }
    }

}
