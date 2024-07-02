using System;
using UnityEngine;

namespace ObjectManage
{
    public class InteractObject : FieldObject, IInteractable
    {
        public event Action OnInteractEvent;
        public Action OnDetectedEvent;
        public Action OnUnDetectedEvent;

        [SerializeField] protected Material _detectMaterial;
        [SerializeField] protected SpriteRenderer _visualRenderer;
        private Material _defaultMaterial;

        

        protected virtual void Start()
        {
            _defaultMaterial = _visualRenderer.material;
            OnDetectedEvent += HandleDetected;
            OnUnDetectedEvent += HandleUnDetected;
        }

        public virtual void Detected()
        {
            OnDetectedEvent?.Invoke();
        }

        public virtual void Interact(InteractData data)
        {
            OnInteractEvent?.Invoke();
        }

        public virtual void UnDetected()
        {
            OnUnDetectedEvent?.Invoke();
        }


        protected void HandleDetected()
        {
            _visualRenderer.material = _detectMaterial;
        }

        protected void HandleUnDetected()
        {
            _visualRenderer.material = _defaultMaterial;
        }
        
    }
}