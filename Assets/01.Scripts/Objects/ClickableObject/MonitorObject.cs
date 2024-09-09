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
            if (DOTween.IsTweening(_monitorPanel)) return;
            Sequence seq = DOTween.Sequence();
            seq.Append(_monitorPanel.DOAnchorPos(_monitorMoveEndPos, _monitorPanelMoveDuration));
            seq.AppendInterval(0.1f);
            seq.Append(_monitorPanel.DOScale(new Vector3(1.5f, 1.5f), _monitorPanelMoveDuration));
            seq.Append(_monitorPanel.DOAnchorPosY(-100f, _monitorPanelMoveDuration));
            seq.SetEase(_monitorPanelMoveEase);
        }
        
        public void MonitorReset()
        {
            if (DOTween.IsTweening(_monitorPanel)) return;
            Sequence seq = DOTween.Sequence();
            seq.Append(_monitorPanel.DOAnchorPos(Vector2.zero, _monitorPanelMoveDuration));
            seq.Append(_monitorPanel.DOScale(new Vector3(1f, 1f), _monitorPanelMoveDuration));
            seq.Append(_monitorPanel.DOAnchorPosY(-5000f, _monitorPanelMoveDuration));
            seq.SetEase(_monitorPanelMoveEase);
        }
    }
}