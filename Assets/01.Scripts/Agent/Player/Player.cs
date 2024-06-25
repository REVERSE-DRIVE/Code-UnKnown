using System;

public class Player : Agent
{
    public PlayerInput PlayerInputCompo { get; protected set; }
    public PlayerVFX PlayerVFXCompo { get; protected set; }
    public PlayerAttackController PlayerAttackCompo { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerInputCompo = GetComponent<PlayerInput>();
        PlayerVFXCompo = VFXCompo  as PlayerVFX;
        PlayerAttackCompo = GetComponent<PlayerAttackController>();
        PlayerAttackCompo.Initialize(this);

    }

    private void Start()
    {
        
    }

    public override void SetDead()
    {
        isDead = true;
        
    }
}
