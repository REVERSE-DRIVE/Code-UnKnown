using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindowEnum
{
    EffectSelect,
    Dark,
    BossCutScene
}

public enum WindowUITypeEnum
{
    Game,
    System,
    Event,
    Boss
}

[Serializable]
public struct WindowData
{
    public WindowEnum window;
    public WindowUITypeEnum windowType;
}

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private WindowData[] windowDatas;
    public Dictionary<WindowEnum, IWindowPanel> panelDictionary;
    [SerializeField] private Transform _canvasTrm;
    private Transform _gameCanvas;
    private Transform _eventCanvas;
    private Transform _systemCanvas;

    private void Awake()
    {
        _gameCanvas = _canvasTrm.Find("GameCanvas");
        _eventCanvas = _canvasTrm.Find("EventCanvas");
        _systemCanvas = _canvasTrm.Find("SystemCanvas");
        
        panelDictionary = new Dictionary<WindowEnum, IWindowPanel>();
        foreach (WindowData window in windowDatas)
        {
            IWindowPanel panel;
            switch (window.windowType)
            {
                case WindowUITypeEnum.Game:
                    panel = _gameCanvas.Find($"{window.window.ToString()}Panel").GetComponent<IWindowPanel>();
                    break;
                case WindowUITypeEnum.System:
                    panel = _systemCanvas.Find($"{window.window.ToString()}Panel").GetComponent<IWindowPanel>();
                    break;
                case WindowUITypeEnum.Event:
                    panel = _eventCanvas.Find($"{window.window.ToString()}Panel").GetComponent<IWindowPanel>();
                    break;
                case WindowUITypeEnum.Boss:
                    panel = _eventCanvas.Find("BossCanvas").Find($"{window.window.ToString()}Panel").GetComponent<IWindowPanel>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            panelDictionary.Add(window.window, panel);
        }
    }

    public void Open(WindowEnum target)
    {
        if (panelDictionary.TryGetValue(target, out IWindowPanel panel))
        {
            panel.Open();
        }
    }

    public void Close(WindowEnum target)
    {
        if (panelDictionary.TryGetValue(target, out IWindowPanel panel))
        {
            panel.Close();
        }
    }
}