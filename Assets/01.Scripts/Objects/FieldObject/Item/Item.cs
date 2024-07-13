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
        
        private readonly int _activeMaterialRadius = Shader.PropertyToID("_Radius");

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
            base.Detected();
            _itemNameText.gameObject.SetActive(true);
        }
        
        public override void UnDetected()
        {
            base.UnDetected();
            _itemNameText.gameObject.SetActive(false);
        }

        public override void Interact(InteractData data)
        {
            base.Interact(data);
            StartCoroutine(InteractCoroutine());
        }

        private IEnumerator InteractCoroutine()
        {
            _visualRenderer.material = _activeMaterial;
            _visualRenderer.material.DOFloat(0, _activeMaterialRadius, 0.5f);
            yield return new WaitForSeconds(0.5f);
            PoolingManager.Instance.Push(this);
        }


        public void ResetItem()
        {
            _itemNameText.gameObject.SetActive(false);
            _visualRenderer.material = _activeMaterial;
            isDetected = false;
            _visualRenderer.material.SetFloat(_activeMaterialRadius, 0);
            _visualRenderer.material.DOFloat(1.5f, _activeMaterialRadius, 0.5f);
        }
    }
}