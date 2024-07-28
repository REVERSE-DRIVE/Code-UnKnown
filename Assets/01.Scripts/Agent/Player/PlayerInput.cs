using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private MainInput _mainInput;
    public ControlButtons controlButtons;
    private HoldButton _actionButton;
    private HoldButton _skillButton;

    public event Action<Vector2> OnMovementEvent;

    [SerializeField] private Vector2 _inputDirection;
    public Vector2 Direction => _inputDirection;
    
    private void Awake()
    {
        _mainInput = new MainInput();
        _mainInput.Player.Move.performed += Move;
        _mainInput.Player.Move.canceled += Cancel;
        _mainInput.Player.Enable();
        _actionButton = controlButtons.actionButton;
        _skillButton = controlButtons.skillButton;
    }

    private void Cancel(InputAction.CallbackContext obj)
    {
        _inputDirection = Vector2.zero;
    }

    private void Update()
    {
        OnMovementEvent?.Invoke(_inputDirection);
    }

    private void Move(InputAction.CallbackContext value)
    {
        _inputDirection = value.ReadValue<Vector2>();
    }

    
}