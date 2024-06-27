using System;

namespace ObjectManage
{
    public interface IInteractable
    {
        public Action OnDetectedEvent { get; protected set; }
        public Action OnUnDetectedEvent { get; protected set; }
        
        public void Interact(InteractData data);
    }
}