using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    public PlayerInput PlayerInputCompo { get; protected set; }
    public PlayerVFX PlayerVFXCompo { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerInputCompo = GetComponent<PlayerInput>();
        PlayerVFXCompo = VFXCompo  as PlayerVFX;
    }

    public override void SetDead()
    {
        throw new System.NotImplementedException();
    }
}
