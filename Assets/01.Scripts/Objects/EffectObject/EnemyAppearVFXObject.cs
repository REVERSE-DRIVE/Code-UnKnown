using System.Collections;
using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{
    
    public class EnemyAppearVFXObject : EffectObject
    {
        [SerializeField] private Transform _impactLineTrm;
        private SpriteRenderer _positionPointerRenderer;
        private Material _pointerMat;
        private int _appearHeightHash;
        private void Awake()
        {
            _positionPointerRenderer = transform.Find("PositionPointer").GetComponent<SpriteRenderer>();
            _pointerMat = _positionPointerRenderer.material;
            _appearHeightHash = Shader.PropertyToID("_AppearHeight");
            
        }

        [ContextMenu("Play")]

        public override void Play()
        {
            OnPlayEvent?.Invoke();
            _pointerMat.SetFloat(_appearHeightHash, 1f);
            StartCoroutine(PlayCoroutine());
        }

        private IEnumerator PlayCoroutine()
        {
            float currentTime = 1;
            while (currentTime > 0f)
            {
                currentTime -= Time.deltaTime;
                _pointerMat.SetFloat(_appearHeightHash, currentTime / 1f);
                yield return null;
            }
            _impactLineTrm.localScale = new Vector3(1.5f, 16f,1);

            _impactLineTrm.DOScaleX(0f, 0.2f);
            yield return new WaitForSeconds(0.1f);

        }

        public override void Stop()
        {
            
        }

        public override void ResetItem()
        {
            base.ResetItem();
            _impactLineTrm.localScale = new Vector3(0f, 16f,1);
        }
    }

}