using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowHeaderUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public event Action<PointerEventData> OnStartDrag;
    public event Action<PointerEventData> OnDraging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnStartDrag?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDraging?.Invoke(eventData);
    }
}
