using System;
using System.Collections;
using DG.Tweening;
using ObjectManage;
using ObjectPooling;
using TMPro;
using UnityEngine;

namespace ItemManage
{
    public class Item : InteractObject, IPoolable
    {
        [field:SerializeField] public PoolingType type { get; set; }
        [field:SerializeField] public ItemSO ItemSO { get; private set; }
        [SerializeField] private TextMeshPro _itemNameText;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Material _activeMaterial;
        public GameObject ObjectPrefab => gameObject;
        
        private bool isInteracted;
        private bool isSpawnig;
        private readonly int _activeMaterialViewOffset = Shader.PropertyToID("_ViewOffset");

        private void Awake()
        {
            _defaultMaterial = _visualRenderer.material;
        }

        public void SetItem(ItemSO itemSO)
        {
            ItemSO = itemSO;
            _itemNameText.text = ItemSO.itemName;
            _visualRenderer.sprite = ItemSO.itemIcon;
            _itemType = ItemSO.itemType;
        }

        public override void Detected()
        {
            if (!isInteracted || !isSpawnig)
            {
                base.Detected();
            }

            ItemNameTextActive(true);
        }
        
        public override void UnDetected()
        {
            if (!isInteracted || !isSpawnig)
            {
                base.UnDetected();
            }

            ItemNameTextActive(false);
        }

        public override void Interact(InteractData data)
        {
            if (isInteracted || isSpawnig) return;
            base.Interact(data);
            StartCoroutine(InteractCoroutine());
        }

        private IEnumerator InteractCoroutine()
        {
            isInteracted = true;
            ChangeActiveMaterial(1.1f, 0f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            PoolingManager.Instance.Push(this);
        }


        public void ResetItem()
        {
            isSpawnig = true;
            ItemNameTextActive(false);
            isDetected = false;
            isInteracted = false;
            ChangeActiveMaterial(0f, 1.1f, 0.5f, () => isSpawnig = false);
        }

        private void ItemNameTextActive(bool isActive)
        {
            _itemNameText.gameObject.SetActive(isActive);
        }

        private void ChangeActiveMaterial(float firstViewOffsetValue, float changeViewOffsetValue, float duration, TweenCallback onCompleteCallback = null)
        {
            _visualRenderer.material = _activeMaterial;
            _visualRenderer.material.SetFloat(_activeMaterialViewOffset, firstViewOffsetValue);
            _visualRenderer.material.DOFloat(changeViewOffsetValue, _activeMaterialViewOffset, duration).OnComplete(onCompleteCallback);
        }
    }
}