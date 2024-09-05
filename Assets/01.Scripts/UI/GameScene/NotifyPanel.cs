using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotifyPanel : MonoBehaviour
{
    Sequence sequence;

    RectTransform _rectTrm;
    Image _image;
    TextMeshProUGUI _content;

    private void Awake() {
        _rectTrm = transform as RectTransform;
        _image = GetComponent<Image>();
        _content = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void Open(TextMeshProUGUI text) {
        
    }

    void Clear() {
        
    }
}
