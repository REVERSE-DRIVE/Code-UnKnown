using UnityEngine;

namespace ObjectPooling
{
    public interface IPoolable
    {
        [field: SerializeField] public PoolingType type { get; protected set; }
        public GameObject thisObject { get; protected set; }

        public void Initialize(GameObject thisObject)
        {
            this.thisObject = thisObject;
        }
        public void ResetItem();
    }
}