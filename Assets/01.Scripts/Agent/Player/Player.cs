using System;
using PlayerPartsManage;
using UnityEngine;

public class Player : Agent
{
    public PlayerInput PlayerInputCompo { get; protected set; }
    public PlayerVFX PlayerVFXCompo { get; protected set; }
    public PlayerAttacker PlayerAttackCompo { get; protected set; }
    public PlayerController PlayerController { get; protected set; }
    public PlayerPartController PlayerPartController { get; protected set; }
    [field:SerializeField] public AdditionalStat additionalStat { get; protected set; }
    
    protected override void Awake()
    {
        base.Awake();
        PlayerInputCompo = GetComponent<PlayerInput>();
        PlayerAttackCompo = GetComponent<PlayerAttacker>();
        PlayerPartController = GetComponent<PlayerPartController>();
        PlayerVFXCompo = VFXCompo  as PlayerVFX;
        PlayerController = MovementCompo as PlayerController;
        
        // PlayerAttackCompo = GetComponent<PlayerAttackController>();
        // PlayerAttackCompo.Initialize(this);

        additionalStat = Instantiate(additionalStat);
        additionalStat.SetOwner(this);
    }

    private void Start()
    {
    }

    public override void SetDead()
    {
        isDead = true;
        
    }
}
