using System;
using System.Collections.Generic;
using UnityEngine;

namespace EffectState
{
    public class EffectStateController : MonoBehaviour
    {
        private Agent _owner;
        private Dictionary<EffectType, EffectState> _effectsDictionary = new Dictionary<EffectType, EffectState>();
        
        // 리플렉션을 활용해서 구현하자
        private void Awake()
        {
            _owner = GetComponent<Agent>();
            
            foreach (EffectType effectEnum in Enum.GetValues(typeof(EffectType)))
            {
                string typeName = effectEnum.ToString();
                Type t = Type.GetType($"EffectState.Effect{typeName}");

                try
                {
                    EffectState effect = Activator.CreateInstance(t, _owner, false) as EffectState;
                    _effectsDictionary.Add(effectEnum, effect);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Effect Controller : no Effect found [ {typeName} ] - {ex.Message}");
                }
            }
            
            
        }

        private void Start()
        {
            _owner.HealthCompo.OnDieEvent.AddListener(ResetEffects);
        }

        private void OnEnable()
        {
            ResetEffects();
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

        public void ResetEffects()
        {
            foreach (EffectState effect in _effectsDictionary.Values)
            {
                effect.ResetEffect();
            }
        }
    }
}