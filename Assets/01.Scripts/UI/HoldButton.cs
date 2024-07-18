using UnityEngine;

public class HoldButton : MonoBehaviour
{
    private MainInput _mainInput;
    [SerializeField] private bool _isHold;
    
    private void Awake()
    {
        _mainInput = new MainInput();
        _mainInput.Player.Attack.performed += _ => _isHold = true;
        _mainInput.Player.Attack.canceled += _ => _isHold = false;
        _mainInput.Player.Attack.Enable();
    }
}