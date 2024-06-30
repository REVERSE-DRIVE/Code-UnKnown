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
        _rigidCompo = agent.GetComponent<Rigidbody2D>();
    }

    public void SetMovement(Vector2 movement)
    {
        
        Vector2 direction = ((Vector3)movement - transform.position).normalized;
        _rigidCompo.velocity = direction * _agent.Stat.moveSpeed;
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