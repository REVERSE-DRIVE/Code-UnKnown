public class PlayerManager : MonoSingleton<PlayerManager>
{
    public Player player { get; private set; }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        
    }
}
