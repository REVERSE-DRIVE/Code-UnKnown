public abstract class Effect
{
    public int level;
    public float duration;
    protected Agent _owner;
    
    
    public abstract void Enter();

    public void Update()
    {
        --duration;
        
    }

    protected abstract void UpdateEffect();

    public abstract void Exit();
}