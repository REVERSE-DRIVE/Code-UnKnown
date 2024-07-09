using System;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{
    public class EffectObject : MonoBehaviour, ILifeTimeLimited, IPoolable
    {
        [SerializeField] private ParticleSystem[] _particles;
        [SerializeField] private bool _playOnSpawn;
        [field: SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        
        [SerializeField] protected float _lifeTime;
        protected float _currentLifeTime;
        private bool _isActive;
        
        
        float ILifeTimeLimited.CurrentLifeTime
        {
            get => _currentLifeTime;
            set => _currentLifeTime = value;
        }

        private void Update()
        {
            if (!_isActive) return;
            _currentLifeTime += Time.deltaTime;
            CheckDie();
        
        }

        public virtual void Initialize(ActionData actionData)
        {
            // 구현해야함   
        }

        public void Initialize(Vector2 position)
        {
            transform.position = position;
        }

        public void Play()
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].Play();
            }
        }

        public void Stop()
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].Stop();
            }
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
            _isActive = false;
            Stop();

            PoolingManager.Instance.Push(this);
        }

        
        public void ResetItem()
        {
            _currentLifeTime = 0;
            if (_playOnSpawn)
            {
                _isActive = true;
                Play();
            }
        }
    }
}