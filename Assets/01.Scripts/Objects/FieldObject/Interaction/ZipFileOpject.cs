using System;
using ItemManage;
using ObjectManage;
using Random = UnityEngine.Random;

public class ZipFileOpject : InteractObject
{
    private void OnEnable()
    {
        OnInteractEvent += HandleInteract;
    }

    private void OnDisable()
    {
        OnInteractEvent -= HandleInteract;
    }

    private void HandleInteract()
    {
        float random = Random.Range(0, 1);
        if (random < 0.5f)
        {
            ItemType type = ItemType.Weapon;
            int id = Random.Range(0, 2);
            ItemDropManager.Instance.DropItem(type, id, transform.position);
        }
        else
        {
            ItemType type = ItemType.Resource;
            int id = Random.Range(0, 2);
            ItemDropManager.Instance.DropItem(type, id, transform.position);
        }
    }
}
