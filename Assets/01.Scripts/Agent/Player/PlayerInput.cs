using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Button _actionButton;
    [SerializeField] private Button _skillButton;

    public Action<Vector2> OnMovementEvent;
    public Action OnActionEvent;
    public Action OnSkillEvent;

    [SerializeField] private Vector2 _inputDirection;
    
    private void Awake()
    {
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