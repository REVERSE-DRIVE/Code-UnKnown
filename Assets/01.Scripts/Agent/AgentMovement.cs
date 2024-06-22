using UnityEngine;

public class AgentMovement : MonoBehaviour, IMovement
{
    protected Agent _agent;
    protected Rigidbody2D _rigidCompo;

    private Vector2 _velocity;
    public Vector2 Velocity => _velocity;
    private Vector2 _movementInput;
    
    public void Initialize(Agent agent)
    {
        _agent = agent;
        
    }

    public void SetMovement(Vector2 movement)
    {
        
        
    }

    public void StopImmediately()
    {
        _rigidCompo.velocity = Vector2.zero;
        
    }

    
    public void GetKnockBack(Vector2 force)
    {
        StopImmediately();
        _rigidCompo.AddForce(force, ForceMode2D.Impulse);
    }
}