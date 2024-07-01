using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : AgentMovement
{
    
    Vector2 _movementInputs;
    
    private void Update()
    {
        Move(_movementInputs.normalized);
    }
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _movementInputs = input;
    }   

    private void Move(Vector2 input)
    {
        transform.Translate(input * (5 * Time.deltaTime));
    }
}