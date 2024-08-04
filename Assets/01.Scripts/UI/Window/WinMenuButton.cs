using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenuButton : MonoBehaviour
{
    [SerializeField] WindowType type;
    Button button;
    Image borderImg;

    WindowUI window;
    
    private void Awake() {
        borderImg = GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(BtnClick);   

        WindowManager.Instance.OnFocus += OnWindowFocus;
    }

    private void OnDestroy() {
        if (WindowManager.Instance)
            WindowManager.Instance.OnFocus -= OnWindowFocus;

        if (window)
            window.OnClose -= OnWindowClose;
    }

    private void Start() {
        window = WindowManager.Instance.GetWindow(type); // 캐싱
        window.OnClose += OnWindowClose;
    }

    private void BtnClick()
    {
        window.Open();
    }

    private void OnWindowFocus(WindowType type) {
        borderImg.color = this.type == type ? Color.cyan : Color.gray;   
    }

    private void OnWindowClose()
    {
        borderImg.color = Color.gray;   
    }
}
