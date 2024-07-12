using System;
using System.Collections;
using ItemManage;
using ObjectManage;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZipFileObject : InteractObject
{
    [SerializeField] private int _resourceDropPercent;
    [SerializeField] private int _weaponDropPercent;
    [SerializeField] private int _minDropAmount;
    [SerializeField] private int _maxDropAmount;
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
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        int dropAmount = Random.Range(_minDropAmount, _maxDropAmount);

        for (int i = 0; i < dropAmount; i++)
        {
            int random = Random.Range(0, 100);
            ItemType type = ItemType.Resource;
            if (random < _resourceDropPercent)
            {
                type = ItemType.Resource;
            
            }
            else if (random < _resourceDropPercent + _weaponDropPercent)
            {
                type = ItemType.Weapon;
            }
            Vector3 position = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            ItemDropManager.Instance.DropItem(type, 0, position);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
