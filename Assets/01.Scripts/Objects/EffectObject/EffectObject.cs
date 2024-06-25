using System;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{
    public class EffectObject : MonoBehaviour, ILifeTimeLimited, IPoolable
    {
        [field: SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        
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
            PoolingManager.Instance.Push(this);
        }

        
        public void ResetItem()
        {
            throw new NotImplementedException();
        }
    }
}