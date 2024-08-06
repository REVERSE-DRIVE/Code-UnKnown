using System;
using UnityEngine;

namespace ObjectManage
{
    public class ClickIcon : ClickableObject
    {
        [SerializeField] private LaptopPanel _openPanel;
        private void Awake()
        {
            OnClickEvent += IconClickEvent;
        }
        
        private void OnDestroy()
        {
            OnClickEvent -= IconClickEvent;
        }

        private void IconClickEvent()
        {
            _openPanel.Open();
        }
    }
}