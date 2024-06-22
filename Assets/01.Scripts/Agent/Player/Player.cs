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
        isDead = true;
        
    }
}
