using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class HoldButton : MonoBehaviour
{
    private MainInput _mainInput;
    [SerializeField] private bool _isHold;
    
    private void Awake()
    {
        _mainInput = new MainInput();
        Start();
    }

    private void Start()
    {
        _mainInput.Player.Attack.performed += Performed;
        _mainInput.Player.Attack.canceled += Canceled;
        _mainInput.Player.Attack.Enable();
    }

    private void Canceled(InputAction.CallbackContext context)
    {
        
    }

    private void Performed(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            Debug.Log("Hold");
        }

        if (context.interaction is TapInteraction)
        {
            Debug.Log("Tap");
        }
    }
}