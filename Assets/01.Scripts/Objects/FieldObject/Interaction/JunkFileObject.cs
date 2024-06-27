using System;
using ObjectManage;
using UnityEngine;

public class JunkFileObject : InteractObject
{
    private Rigidbody2D _rigidCompo;
    [SerializeField] private float _pushPower;
    private Vector2 _origin;
    
    private void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();

        OnInteractEvent += HandlePush;
    }

    public override void Interact(InteractData data)
    {
        base.Interact(data);
        _origin = data.interactOriginPosition;
    }

    private void HandlePush()
    {
        Vector2 direction = (Vector2)transform.position - _origin;
        _rigidCompo.AddForce(direction.normalized * _pushPower, ForceMode2D.Impulse);
    }
}