using UnityEngine;

namespace EffectState
{
    [System.Serializable]
    public abstract class EffectState
    {
        public bool isEffectEnabled;
        public float duration;
        public int effectLevel;
        protected float _currentTime = 0;

        protected Agent _owner;

        public EffectState(Agent agent)
        {
            _owner = agent;
        }

        public void Apply(float time)
        {
            if (!isEffectEnabled)
            {
                isEffectEnabled = true;
                Enter();
                duration = time;
                return;
            }
            duration += time;
        }
        
        public void Apply(float time, int level)
        {
            Apply(time);
            effectLevel = level;
        }
        
        public abstract void Enter();

        public void Update()
        {
            duration -= Time.deltaTime;
            _currentTime += Time.deltaTime;

            if (_currentTime >= 1)
            {
                _currentTime = 0;
                UpdateStateBySecond();
            }
            UpdateState();
        }

        protected abstract void UpdateState();
        protected abstract void UpdateStateBySecond();
        
        public abstract void Over();

        public void ResetEffect()
        {
            duration = 0;
            isEffectEnabled = false;
            _currentTime = 0;
        }
    }
}