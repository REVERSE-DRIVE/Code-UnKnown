using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AgentMovement : MonoBehaviour, IMovement
{
    protected Agent _agent;
    protected Rigidbody2D _rigidCompo;

    protected Vector2 _velocity;
    public Vector2 Velocity => _velocity;
    protected Vector2 _movementInput;
    protected Transform _visualTrm;
    public bool isStun = false;
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
        if (isStun) return;
        //if (movement.magnitude < 0.1f)
        int speed = _agent.Stat.moveSpeed.GetValue();
        speed = speed < 0 ? 0: speed;
        _velocity = movement.normalized * speed;
        _rigidCompo.velocity = _velocity;

    }

    public void StopImmediately()
    {
        _rigidCompo.velocity = Vector2.zero;
        
    }

    
    public void GetKnockBack(Vector2 force, float duration)
    {
        StartCoroutine(KnockBackCoroutine(force, duration));
    }

    private IEnumerator KnockBackCoroutine(Vector2 force, float duration)
    {
        Vector2 beforeVelocity = _rigidCompo.velocity;
        isStun = true;
        StopImmediately();
        float currentTime = 0;
        float ratio = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            ratio = currentTime / duration;
            _rigidCompo.velocity = Vector2.Lerp(force, Vector2.zero, ratio);
            yield return null;
        }
        StopImmediately();
        isStun = false;
        _rigidCompo.AddForce(force, ForceMode2D.Impulse);

    }
}