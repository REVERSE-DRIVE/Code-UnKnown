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
        
        protected bool _isInteracted;
        protected bool _isSpawnig;
        private readonly int _activeMaterialViewOffset = Shader.PropertyToID("_ViewOffset");

        private void Awake()
        {
            _defaultMaterial = _visualRenderer.material;
        }

        public virtual void SetItem(ItemSO itemSO)
        {
            ItemSO = itemSO;
            if (_itemNameText != null)
                _itemNameText.text = ItemSO.itemName;
            _visualRenderer.sprite = ItemSO.itemIcon;
        }

        public override void Detected()
        {
            if (!_isInteracted || !_isSpawnig)
            {
                base.Detected();
            }

            ItemNameTextActive(true);
        }
        
        public override void UnDetected()
        {
            if (!_isInteracted || !_isSpawnig)
            {
                base.UnDetected();
            }

            ItemNameTextActive(false);
        }

        public override void Interact(InteractData data)
        {
            if (_isInteracted || _isSpawnig) return;
            
            _isInteracted = true;
            base.Interact(data);
            StartCoroutine(InteractCoroutine());
        }

        protected IEnumerator InteractCoroutine()
        {
            ChangeActiveMaterial(1.1f, 0f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            PoolingManager.Instance.Push(this);
        }


        public void ResetItem()
        {
            _isSpawnig = true;
            isDetected = false;
            _isInteracted = false;
            ItemNameTextActive(false);
            ChangeActiveMaterial(0f, 1.1f, 0.5f, () => _isSpawnig = false);
        }

        private void ItemNameTextActive(bool isActive)
        {
            if (_itemNameText != null)
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