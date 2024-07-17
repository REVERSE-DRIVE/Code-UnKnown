using System;
using UnityEngine;

public class Player : Agent
{
    public PlayerInput PlayerInputCompo { get; protected set; }
    public PlayerVFX PlayerVFXCompo { get; protected set; }
    public PlayerAttackController PlayerAttackCompo { get; protected set; }

    [field:SerializeField] public AdditionalStat additionalStat { get; protected set; }
    
    protected override void Awake()
    {
        base.Awake();
        PlayerInputCompo = GetComponent<PlayerInput>();
        PlayerVFXCompo = VFXCompo  as PlayerVFX;
        PlayerAttackCompo = GetComponent<PlayerAttackController>();
        PlayerAttackCompo.Initialize(this);
        
        additionalStat = Instantiate(additionalStat);
    }

    private void Start()
    {
    }

    public override void SetDead()
    {
        isDead = true;
        
    }
}
