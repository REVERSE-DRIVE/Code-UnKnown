using System;
using UnityEngine;

namespace ObjectManage
{
    
    public class EffectObject : MonoBehaviour, ILifeTimeLimited
    {
        [SerializeField] protected float _lifeTime;
        protected float _currentLifeTime;
    
        
        
        float ILifeTimeLimited.CurrentLifeTime
        {
            get => _currentLifeTime;
            set => _currentLifeTime = value;
        }

        private void Update()
        {
            _currentLifeTime += Time.deltaTime;
        
        
        }


        public void CheckDie()
        {
            if (_currentLifeTime >= _lifeTime)
            {
                HandleDie();
            }
        }

        public void HandleDie()
        {
            // 풀링을 통해 없애야한다
        }
    }
}