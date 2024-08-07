using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowUI : MonoBehaviour, IPointerDownHandler
{

    [field: SerializeField] public WindowType Type {  get; private set; }
    [SerializeField] WindowHeaderUI header;
    [SerializeField] Button close;

    public bool IsOpen { get => canvasGroup.alpha > 0; }

    public event System.Action OnFocus;
    public event System.Action OnClose;

    Canvas canvas;
    CanvasGroup canvasGroup;
    private RectTransform _rectTransform;

    protected virtual void Awake() {
        header.OnStartDrag += OnBeginDrag;
        header.OnDraging += OnDrag;

        close.onClick.AddListener(Close);
        
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = transform as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        (transform as RectTransform).anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData) => OnPointerDown(eventData, true);

    public void OnPointerDown(PointerEventData eventData, bool focusEvent) {
        if (focusEvent && transform.parent.childCount - 1 != transform.GetSiblingIndex()) {
            OnFocus?.Invoke();
        }
        
        (transform as RectTransform).SetAsLastSibling(); // 맨아래로
    }
    
    public void Open() {
        // gameObject.SetActive(true);
        // if (IsOpen) {
            // return;
        // }

        //canvasGroup.alpha = 1;
        //canvasGroup.blocksRaycasts = true;
        //canvasGroup.interactable = true;
        PlayAnimation(1);
        OnPointerDown(null, false); // 여는 동시에 맨앞
        OnFocus?.Invoke(); // focus 이벤트 무조건 함
    }

    public void Close()
    {
        // gameObject.SetActive(false);
        //canvasGroup.alpha = 0;
        //canvasGroup.blocksRaycasts = false;
        //canvasGroup.interactable = false;
        PlayAnimation(0);
        OnClose?.Invoke();
    }

    private void PlayAnimation(float endValue)
    {
        _rectTransform.DOScaleY(endValue, 0.5f);
    }
}
