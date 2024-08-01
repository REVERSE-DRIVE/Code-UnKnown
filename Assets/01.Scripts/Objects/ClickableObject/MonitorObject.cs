using System;
using DG.Tweening;
using UnityEngine;

namespace ObjectManage
{
    public class MonitorObject : ClickableObject
    {
        [SerializeField] private RectTransform _monitorPanel;
        [SerializeField] private float _monitorPanelMoveDuration;
        [SerializeField] private Ease _monitorPanelMoveEase;
        [SerializeField] private Vector2 _monitorMoveEndPos;
        private void Awake()
        {
            OnClickEvent += MonitorClickEvent;
        }

        private void OnDestroy()
        {
            OnClickEvent -= MonitorClickEvent;
        }

        private void MonitorClickEvent()
        {
            _monitorPanel.DOAnchorPos(_monitorMoveEndPos, _monitorPanelMoveDuration)
                .SetEase(_monitorPanelMoveEase);
        }
    }
}