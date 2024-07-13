using System;
using UnityEngine;

public class AgentMovement : MonoBehaviour, IMovement
{
    protected Agent _agent;
    protected Rigidbody2D _rigidCompo;

    protected Vector2 _velocity;
    public Vector2 Velocity => _velocity;
    protected Vector2 _movementInput;
    protected Transform _visualTrm;


    private void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();
        _visualTrm = transform.Find("Visual");
    }

    public void Initialize(Agent agent)
    {
        _agent = agent;
        
    }

    public void SetMovement(Vector2 movement)
    {
        _velocity = movement.normalized * _agent.Stat.moveSpeed.GetValue();
        _rigidCompo.velocity = _velocity;

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