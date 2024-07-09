using System;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectManage
{
    public class InteractObject : FieldObject, IInteractable
    {
        public UnityEvent OnOutsideInteractEvent;
        public event Action OnInteractEvent;
        public Action OnDetectedEvent;
        public Action OnUnDetectedEvent;

        [SerializeField] protected Material _detectMaterial;
        [SerializeField] protected SpriteRenderer _visualRenderer;
        protected Material _defaultMaterial;
        public bool isDetected;
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
            OnOutsideInteractEvent?.Invoke();
        }

        public virtual void UnDetected()
        {
            OnUnDetectedEvent?.Invoke();
        }


        protected void HandleDetected()
        {
            if (isDetected) return;
            print("Detected");
            isDetected = true;
            _visualRenderer.material = _detectMaterial;
        }

        protected void HandleUnDetected()
        {
            if (!isDetected) return;
            print("Undetected");
            isDetected = false;
            _visualRenderer.material = _defaultMaterial;
        }
        
    }
}