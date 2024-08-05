using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindowType {
    None,
    Body,
    Leg,
    Core,
    SlamShop
}

public class WindowManager : MonoSingleton<WindowManager>
{
    public event System.Action<WindowType> OnFocus;
    
    [SerializeField] WindowUI[] windows; // 운영체제 아님

    Dictionary<WindowType, WindowUI> windowIndex;
    
    private void Awake() {
        windowIndex = new();
        
        // 인덱싱
        foreach (var item in windows) {
            windowIndex[item.Type] = item;
            item.OnFocus += () => OnFocus?.Invoke(item.Type);
        }
    }

    public WindowUI GetWindow(WindowType type) {
        windowIndex.TryGetValue(type, out var item);
        return item;
    }
}
