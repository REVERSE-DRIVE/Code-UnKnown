using System;
using UnityEngine;

namespace ObjectManage
{
    public class InteractObject : FieldObject, IInteractable
    {
        public event Action OnInteractEvent;
        private Action _onDetectedEvent;
        private Action _onUnDetectedEvent;

        [SerializeField] protected Material _detectMaterial;
        [SerializeField] protected SpriteRenderer _visualRenderer;
        private Material _defaultMaterial;

        Action IInteractable.OnDetectedEvent
        {
            get => _onDetectedEvent;
            set => _onDetectedEvent = value;
        }

        Action IInteractable.OnUnDetectedEvent
        {
            get => _onUnDetectedEvent;
            set => _onUnDetectedEvent = value;
        }

        protected virtual void Start()
        {
            _defaultMaterial = _visualRenderer.material;
            _onDetectedEvent += HandleDetected;
            _onUnDetectedEvent += HandleUnDetected;
        }

        

        public void Interact(Agent interactOwner)
        {
            OnInteractEvent?.Invoke();
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