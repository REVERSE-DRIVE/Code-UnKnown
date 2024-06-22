public interface ILifeTimeLimited
{
    public float CurrentLifeTime { get; protected set; }


    public void CheckDie();
    public void HandleDie();
}