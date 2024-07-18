using UnityEngine;

public interface IMovement
{
    public Vector2 Velocity { get; }

    public void Initialize(Agent agent);
    public void SetMovement(Vector2 movement);
    public void StopImmediately();
    //public void SetDestination(Vector2 destination);
    public void GetKnockBack(Vector2 force, float duration);
    
}