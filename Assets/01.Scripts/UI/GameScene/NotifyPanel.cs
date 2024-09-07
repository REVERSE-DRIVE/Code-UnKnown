using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotifyPanel : MonoSingleton<NotifyPanel>
{
    Sequence sequence;
    IEnumerator timeHandler;

    RectTransform _rectTrm;
    Image _image;
    TextMeshProUGUI _content;
    CanvasGroup _canvasGroup;

    Vector2 originSize;

    protected override void Awake() {
        base.Awake();
        
        _rectTrm = transform as RectTransform;
        _image = GetComponent<Image>();
        _content = GetComponentInChildren<TextMeshProUGUI>();
        _canvasGroup = GetComponent<CanvasGroup>();

        originSize = _rectTrm.sizeDelta;
        Clear();
    }

    public void Show(string text, float time = 0) {
        _content.text = text;

        if (sequence != null)
            sequence.Kill();

        if (timeHandler != null) {
            StopCoroutine(timeHandler);
            timeHandler = null;
        }

        sequence = DOTween.Sequence();

        _canvasGroup.alpha = 1;
        
        Vector2 nowSize = _rectTrm.sizeDelta;
        sequence.Join(_rectTrm.DOSizeDelta(new Vector2(originSize.x, nowSize.y), 0.2f));
        sequence.Append(_rectTrm.DOSizeDelta(new Vector2(originSize.x, originSize.y), 0.2f));


        sequence.Append(_content.DOFade(1, 0));
        sequence.AppendInterval(0.1f);
        sequence.Append(_content.DOFade(0, 0));
        sequence.AppendInterval(0.1f);
        sequence.Append(_content.DOFade(1, 0));

        if (time > 0) {
            timeHandler = CloseWaitHandle(time);
            StartCoroutine(timeHandler);
        }
    }

    public void Hide() {
        if (sequence != null)
            sequence.Kill();

        if (timeHandler != null) {
            StopCoroutine(timeHandler);
            timeHandler = null;
        }

        sequence = DOTween.Sequence();

        sequence.Append(_content.DOFade(0, 0));
        sequence.AppendInterval(0.1f);
        sequence.Append(_content.DOFade(1, 0));
        sequence.AppendInterval(0.1f);
        sequence.Append(_content.DOFade(0, 0));
        
        sequence.Join(_rectTrm.DOSizeDelta(new Vector2(originSize.x, originSize.y / 5f), 0.2f));
        sequence.Append(_rectTrm.DOSizeDelta(new Vector2(0, originSize.y / 5f), 0.2f));

        sequence.OnComplete(() => {
            sequence = null;
            Clear();
        });
    }

    void Clear() {
        _canvasGroup.alpha = 0;
        _rectTrm.sizeDelta = new Vector2(originSize.y / 5f, originSize.y / 5f);

        Color textColor = _content.color;
        textColor.a = 0;
        _content.color = textColor;
    }
    
    IEnumerator CloseWaitHandle(float time) {
        yield return new WaitForSeconds(time);
        Hide();
    }
}
