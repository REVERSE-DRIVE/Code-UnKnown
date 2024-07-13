using System;
using System.Collections.Generic;
using UnityEngine;

namespace EffectState
{
    public class EffectStateController : MonoBehaviour
    {
        [SerializeField] private Agent _owner;
        [SerializeField] private List<EffectState> _currentEffectsList;
        
        // 리플렉션을 활용해서 구현하자
        
        private void Update()
        {
            for (int i = 0; i < _currentEffectsList.Count; i++)
            {
                EffectState effect = _currentEffectsList[i];
                effect.Update();
                if (effect.duration <= 0)
                {
                    effect.Over();
                    effect.isEffectEnabled = false;
                    
                }
            }
        }


        public void ApplyEffect(EffectType type)
        {
            // 효과를 실질적으로 적용해주는 코드를 작성해야함
        }
    }
}