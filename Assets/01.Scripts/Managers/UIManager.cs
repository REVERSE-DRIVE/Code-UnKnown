using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindowEnum
{
    EffectSelect,
    Dark,
    BossCutScene,
    Clear,
    Editor,
    Minimap,
    StageChange
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

    [Header("Feedbacks")]
    [SerializeField] private FeedbackPlayer _clickFeedback;

    private StageChangePanel _stageChangePanel;
    private Transform _gameCanvas;
    private Transform _eventCanvas;
    private Transform _systemCanvas;

    public bool isPause;
    public bool isEffectSelecting;
    public bool IsTimeStopped => isPause || isEffectSelecting;

    protected override void Awake()
    {
        base.Awake();

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

        _stageChangePanel = panelDictionary[WindowEnum.StageChange] as StageChangePanel;
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

    public IWindowPanel GetPanel(WindowEnum type)
    {
        if (panelDictionary.TryGetValue(type, out IWindowPanel panel))
        {
            return panel;
        }
        return null;
    }

    public void ShowStageChange()
    {
        StartCoroutine(ShowStageChangeCoroutine());
    }

    private IEnumerator ShowStageChangeCoroutine()
    {
        Open(WindowEnum.StageChange);
        _stageChangePanel.SetZeroGauge();
        yield return new WaitForSeconds(0.6f);
        _stageChangePanel.FillGauge();
        yield return new WaitForSeconds(2.6f);
        Close(WindowEnum.StageChange);
        CameraManager.Instance.SetRotationToDefault(180, 1f);
        GameManager.Instance.ResetPlayer();
    }

    public void PlayClickSFX()
    {
        _clickFeedback.PlayFeedback();
    }
}