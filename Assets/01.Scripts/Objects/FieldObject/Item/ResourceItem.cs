using System;
using DG.Tweening;
using ItemManage;
using QuestManage;
using UnityEngine;

public class ResourceItem : Item
{
    [SerializeField] private ResourceRank _resourceRank;
    [SerializeField] private float _speed;
    
    public override void SetItem(ItemSO itemSO)
    {
        base.SetItem(itemSO);
        _resourceRank = itemSO.resourceRank;
    }

    private void Update()
    {
        FollowPlayer();
    }
    
    private void FollowPlayer()
    {
        // 마인크래프트 exp 스타일로 플레이어를 따라다님
        if (_isInteracted) return;
        Vector3 targetPos = PlayerManager.Instance.player.transform.position;
        float distance = (targetPos - transform.position).magnitude;
        if (distance > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInteracted = true;
            Vector3 targetPos = PlayerManager.Instance.player.transform.position;
            transform.DOJump(targetPos + new Vector3(0, 1, 0), 1, 1, 0.5f).OnComplete(() =>
            {
                ResourceManager.Instance.AddResource(ItemSO.resourceValue);
                LevelManager.Instance.ApplyExp(ItemSO.resourceValue);
                //QuestObserver.Instance.CollectTrigger(ItemType.Resource, 1);
                PoolingManager.Instance.Push(this);
            });
        }
    }

    public override void Interact(InteractData data)
    {
        //if (_isInteracted) return;
        //base.Interact(data);
        //LevelManager.Instance.ApplyExp(ItemSO.resourceValue);
    }
}