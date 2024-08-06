using System;
using UnityEngine;

namespace TitleScene
{
    public class TitleSceneManager : MonoSingleton<TitleSceneManager>
    {
        [SerializeField] private StartButtonPanel _startButtonPanel;
        
        private void Start()
        {
            Time.timeScale = 1f;
            _startButtonPanel.Open();
        }
    }
}