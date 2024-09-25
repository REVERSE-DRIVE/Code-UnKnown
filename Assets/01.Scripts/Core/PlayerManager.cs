public class PlayerManager : MonoSingleton<PlayerManager>
{
    public Player player { get; private set; }
    public bool IsFirstSpawn { get; private set; } = true;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        
    }

    // 플레이어 살리기
    public void Revival() {
        IsFirstSpawn = false;
        player.HealthCompo.SetHealth(player.HealthCompo.maxHealth);
    }
}
