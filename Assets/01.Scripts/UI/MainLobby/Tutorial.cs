using System;
using ObjectManage;
using UnityEngine;

public class Tutorial : ClickableObject
{
    [SerializeField] private FadeInOut _fadeInOut;
    private void Awake()
    {
        OnClickEvent += OnClick;
    }

    private void OnDestroy()
    {
        OnClickEvent -= OnClick;
    }

    private void OnClick()
    {
        _fadeInOut.Fade(0.5f, 1f, () =>
        {
            LoadManager.Instance.StartLoad("TutorialScene");
        });
    }
}