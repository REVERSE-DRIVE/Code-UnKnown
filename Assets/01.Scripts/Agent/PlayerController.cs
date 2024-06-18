using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector2 _direction;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_direction * (Time.deltaTime * speed));
    }

    private void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
    }
}