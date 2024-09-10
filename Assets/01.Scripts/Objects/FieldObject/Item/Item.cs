using System;
using System.Collections;
using DG.Tweening;
using ObjectManage;
using ObjectPooling;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ItemManage
{
    public class Item : InteractObject, IPoolable
    {
        public UnityEvent OnInteractEvent;
        [field:SerializeField] public PoolingType type { get; set; }
        [field:SerializeField] public ItemSO ItemSO { get; private set; }
        [SerializeField] private TextMeshPro _itemNameText;
        [SerializeField] private ItemType _itemType;
        public GameObject ObjectPrefab => gameObject;
        
        protected bool _isInteracted;

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
            if (!_isInteracted)
            {
                base.Detected();
            }

            ItemNameTextActive(true);
        }
        
        public override void UnDetected()
        {
            if (!_isInteracted)
            {
                base.UnDetected();
            }

            ItemNameTextActive(false);
        }

        public override void Interact(InteractData data)
        {
            if (_isInteracted) return;
            Debug.Log("Interact");
            _isInteracted = true;
            StartCoroutine(InteractCoroutine());
            base.Interact(data);
        }

        protected IEnumerator InteractCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            PoolingManager.Instance.Push(this);
        }


        public void ResetItem()
        {
            isDetected = false;
            _isInteracted = false;
            ItemNameTextActive(false);
        }

        private void ItemNameTextActive(bool isActive)
        {
            if (_itemNameText != null)
                _itemNameText.gameObject.SetActive(isActive);
        }
    }
}