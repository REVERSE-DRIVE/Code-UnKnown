using ItemManage;
using UnityEngine;

public class ZipFileManager : MonoSingleton<ZipFileManager>
{

    public void SetZipFileSetting(ZipFileObject zip)
    {
        Debug.Log("SetZipFileSetting");
        float random = Random.Range(0f, 1f);
        if (random < 0.9f)
        {
            Debug.Log("SetZipFileSetting 1");
            int partId = Random.Range(0, 8);
            zip.SetZip(ItemType.Part, partId, 1, 1);
        }
        else
        {
            int itemId = 0;
            zip.SetZip(ItemType.Resource, itemId, 3, 5);
        }
    }
}