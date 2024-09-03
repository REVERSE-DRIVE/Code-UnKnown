using System.Collections;
using ObjectManage;
using UnityEngine;

public class StageHoleObject : InteractObject
{
    private void Awake()
    {
        OnInteractEvent += HandleStageChange;
    }

    private void HandleStageChange()
    {
       
        UIManager.Instance.ShowStageChange();

        StartCoroutine(HandleStageChangeCoroutine());
    }

    private IEnumerator HandleStageChangeCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        MapManager.Instance.Clear();
        PoolingManager.Instance.ResetPool();
        MapManager.Instance.Generate();
        Destroy(gameObject);

    }
}
