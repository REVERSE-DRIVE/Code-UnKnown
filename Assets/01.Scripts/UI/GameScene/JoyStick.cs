using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image _stickBG;
    [SerializeField] private OnScreenStick _stick;
    
    private RectTransform _stickBGRect;
    private RectTransform _baseRect;

    private void Awake()
    {
        _stickBGRect = _stickBG.GetComponent<RectTransform>();
        _baseRect = GetComponent<RectTransform>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _baseRect, eventData.position,
                eventData.pressEventCamera, out Vector2 localVector))
        {
            _stickBGRect.anchoredPosition = localVector - _baseRect.sizeDelta / 2;
        }
        _stickBG.gameObject.SetActive(true);
        _stick.OnPointerDown(eventData);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        _stickBG.gameObject.SetActive(false);
        _stick.OnPointerUp(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _stick.OnDrag(eventData);
    }
}
