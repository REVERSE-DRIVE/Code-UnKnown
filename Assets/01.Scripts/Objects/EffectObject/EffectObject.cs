using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{
    public abstract class EffectObject : MonoBehaviour, ILifeTimeLimited, IPoolable
    {
        [SerializeField] protected bool _playOnSpawn;
        [field: SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;
        
        [SerializeField] protected float _lifeTime;
        protected float _currentLifeTime;
        protected bool _isActive;
        
        
        float ILifeTimeLimited.CurrentLifeTime
        {
            get => _currentLifeTime;
            set => _currentLifeTime = value;
        }

        protected void Update()
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

        public abstract void Play();

        public abstract void Stop();
        


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


        public virtual void ResetItem()
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