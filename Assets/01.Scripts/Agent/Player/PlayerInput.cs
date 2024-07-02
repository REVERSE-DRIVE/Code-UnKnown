using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public ControlButtons controlButtons;
    private Button _actionButton;
    private Button _skillButton;

    public event Action<Vector2> OnMovementEvent;
    public event Action OnActionEvent;
    public event Action OnSkillEvent;

    [SerializeField] private Vector2 _inputDirection;
    
    private void Awake()
    {
        _actionButton = controlButtons.actionButton;
        _skillButton = controlButtons.skillButton;
        
        _actionButton.onClick.AddListener(HandleActionEvent);
        _skillButton.onClick.AddListener(HandleSkillEvent);
        
    }

    private void Update()
    {
        OnMovementEvent?.Invoke(_inputDirection);
    }

    public void OnMove(InputValue value)
    {
        _inputDirection = value.Get<Vector2>();
    }

    public void HandleActionEvent()
    {
        OnActionEvent?.Invoke();
    }

    public void HandleSkillEvent()
    {
        OnSkillEvent?.Invoke();
    }


}