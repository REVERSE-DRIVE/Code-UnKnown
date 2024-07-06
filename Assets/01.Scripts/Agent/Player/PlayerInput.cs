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

    [SerializeField] private Vector2 _inputDirection;
    
    private void Awake()
    {
        _actionButton = controlButtons.actionButton;
        _skillButton = controlButtons.skillButton;
    }

    private void Update()
    {
        OnMovementEvent?.Invoke(_inputDirection);
    }

    public void OnMove(InputValue value)
    {
        _inputDirection = value.Get<Vector2>();
    }

    
}