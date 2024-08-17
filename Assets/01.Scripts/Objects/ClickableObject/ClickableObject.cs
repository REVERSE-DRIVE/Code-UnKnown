using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ObjectManage
{
    public class ClickableObject : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClickEvent;
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
            Debug.Log($"[{eventData.pointerCurrentRaycast.gameObject.name}] Clicked");
        }
    }
}