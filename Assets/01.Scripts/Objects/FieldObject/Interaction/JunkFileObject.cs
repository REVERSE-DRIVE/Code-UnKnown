using System;
using ObjectManage;
using UnityEngine;

public class JunkFileObject : InteractObject
{
    private Rigidbody2D _rigidCompo;
    [SerializeField] private float _pushPower;
    private void Awake()
    {
        _rigidCompo = GetComponent<Rigidbody2D>();

        OnInteractEvent += HandlePush;
    }

    private void HandlePush()
    {
        _rigidCompo.AddForce();
    }
}