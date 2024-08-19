using System;
using System.Collections.Generic;
using UnityEngine;

namespace EffectState
{
    public class EffectStateController : MonoBehaviour
    {
        private Agent _owner;
        private Dictionary<EffectType, EffectState> _effectsDictionary;
        
        // 리플렉션을 활용해서 구현하자
        private void Awake()
        {
            _owner = GetComponent<Agent>();
            
            foreach (EffectType effectEnum in Enum.GetValues(typeof(EffectType)))
            {
                string typeName = effectEnum.ToString();
                Type t = Type.GetType($"EffectState.Effect{typeName}State");

                try
                {
                    _effectsDictionary.Add(effectEnum, Activator.CreateInstance(t) as EffectState);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Effect Controller : no Effect found [ {typeName} ] - {ex.Message}");
                }
            }
        }

        private void OnEnable()
        {
            foreach (EffectState effect in _effectsDictionary.Values)
            {
                effect.ResetEffect();
            }
        }

        private void Update()
        {
            foreach (EffectState effect in _effectsDictionary.Values)
            {
                if(!effect.isEffectEnabled) continue;
                effect.Update();
                if (effect.duration <= 0)
                {
                    effect.Over();
                    effect.isEffectEnabled = false;
                    
                }
            }
        }


        public void ApplyEffect(EffectType type, int level = 1, float duration = 5f)
        {
            // 효과를 실질적으로 적용해주는 코드를 작성해야함
            if (_effectsDictionary.TryGetValue(type, out EffectState effect))
            {
                effect.Apply(duration, level);
            }
        }
    }
}