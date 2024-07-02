using System;

namespace ObjectManage
{
    public interface IInteractable // 구조 바꿔야하나
    {
        // 얘네가 private이됨
        public Action OnDetectedEvent { get; protected set; }
        public Action OnUnDetectedEvent { get; protected set; }
        
        public void Interact(InteractData data);
    }
}