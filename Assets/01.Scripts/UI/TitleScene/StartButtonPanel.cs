using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TitleScene
{
    public class StartButtonPanel : MonoBehaviour, IWindowPanel
    {
        [Header("Setting Values")]
        [SerializeField] private float _disableYPos;
        [SerializeField] private float _activeYPos;
        [SerializeField] private float _duration;
        private Button _button;
        private RectTransform _rectTrm;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _rectTrm = transform as RectTransform;
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _button.onClick.AddListener(HandleClickEvent);
        }

        private void HandleClickEvent()
        {
            // 클릭했을때 각종 메뉴들을 띄워주는 이벤트를 호출한다.
            Close();
        }

        public void Open()
        {
            _rectTrm.DOAnchorPosY(_activeYPos, _duration);
            _canvasGroup.DOFade(1f, _duration);
        }

        public void Close()
        {
            _rectTrm.DOAnchorPosY(_disableYPos, _duration);
            _canvasGroup.DOFade(0f, _duration);
        }
    }

}