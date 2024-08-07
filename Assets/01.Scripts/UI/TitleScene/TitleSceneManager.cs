using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TitleScene
{
    public class TitleSceneManager : MonoSingleton<TitleSceneManager>
    {
        public Dictionary<TitleWindowTypeEnum, IWindowPanel> panelDictionary;
        [SerializeField] private Transform _canvasTrm;

        
        private void Awake()
        {
            panelDictionary = new Dictionary<TitleWindowTypeEnum, IWindowPanel>();
            foreach (TitleWindowTypeEnum windowEnum in Enum.GetValues(typeof(TitleWindowTypeEnum)))
            {
                IWindowPanel panel;
                try
                {
                    panel = _canvasTrm.Find($"{windowEnum.ToString()}Panel").GetComponent<IWindowPanel>();
                }
                catch (Exception e)
                {
                    panel = _canvasTrm.Find("BossCanvas").Find($"{windowEnum.ToString()}Panel").GetComponent<IWindowPanel>();
                }
            
                panelDictionary.Add(windowEnum, panel);
            }
        }

        private void Start()
        {
            Time.timeScale = 1f;
            StartCoroutine(TitleStartCoroutine());
        }

        private IEnumerator TitleStartCoroutine()
        {
            panelDictionary[TitleWindowTypeEnum.Dark].Close();
            yield return new WaitForSeconds(2f);
            panelDictionary[TitleWindowTypeEnum.StartButton].Open();

        }

        public void Open(TitleWindowTypeEnum target)
        {
            if (panelDictionary.TryGetValue(target, out IWindowPanel panel))
            {
                panel.Open();
            }
        }

        public void Close(TitleWindowTypeEnum target)
        {
            if (panelDictionary.TryGetValue(target, out IWindowPanel panel))
            {
                panel.Close();
            }
        }
    }
}