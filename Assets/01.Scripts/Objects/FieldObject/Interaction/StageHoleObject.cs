using ObjectManage;

public class StageHoleObject : InteractObject
{
    private void Awake()
    {
        OnInteractEvent += HandleStageChange;
    }

    private void HandleStageChange()
    {
        MapManager.Instance.Clear();
        MapManager.Instance.Generate();
        PoolingManager.Instance.ResetPool();
        Destroy(gameObject);
    }
}
