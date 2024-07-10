using ItemManage;
using ObjectManage;

public class ZipFileOpject : InteractObject
{
    private void OnEnable()
    {
        OnInteractEvent += HandleInteract;
    }

    private void HandleInteract()
    {
        float random = UnityEngine.Random.Range(0, 1);
        if (random < 0.5f)
        {
            ItemType type = ItemType.Weapon;
            
        }
        else
        {
            print("Fail");
        }
    }
}
