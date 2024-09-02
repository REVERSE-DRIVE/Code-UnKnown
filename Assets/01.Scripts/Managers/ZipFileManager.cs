using ItemManage;
using UnityEngine;

public class ZipFileManager : MonoSingleton<ZipFileManager>
{
    [SerializeField] private float _zipfileIsPartRate = 0.9f;
    public void SetZipFileSetting(ZipFileObject zip)
    {
        float random = Random.Range(0f, 1f);
        if (random < _zipfileIsPartRate)
        {
            int partId = Random.Range(0, 8);
            zip.SetZip(ItemType.Part, partId, 1, 1);
        }
        else
        {
            int itemId = 0;
            zip.SetZip(ItemType.Resource, itemId, 2, 5);
        }
    }
}