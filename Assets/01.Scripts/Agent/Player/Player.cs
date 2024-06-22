using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    public PlayerVFX PlayerVFXCompo { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerVFXCompo = VFXCompo  as PlayerVFX;
    }
    
    
    
}
