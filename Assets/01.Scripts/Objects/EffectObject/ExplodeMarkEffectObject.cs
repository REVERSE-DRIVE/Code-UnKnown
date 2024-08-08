using System.Collections;
using UnityEngine;

namespace ObjectManage
{
    public class ExplodeMarkEffectObject: EffectObject
    {
        private Transform _visualTrm;
        private SpriteRenderer _spriteRenderer;
        private Material _material;
        private int _dissolveHash;
        private int _noiseHash;
        [SerializeField] private float _dissolveDelay = 4f;
        [SerializeField] private float _dissolveDuration = 1f;
        [SerializeField] private Vector2 _noiseRandomRange;

        private void Awake()
        {
            _dissolveHash = Shader.PropertyToID("_Dissolve");
            _noiseHash = Shader.PropertyToID("_LineDensity");
            _visualTrm = transform.Find("Visual");
            _spriteRenderer = _visualTrm.GetComponent<SpriteRenderer>();
            _material = _spriteRenderer.material;
        }
        
        
        
        public override void Play()
        {
            _material.SetFloat(_noiseHash, Random.Range(_noiseRandomRange.x, _noiseRandomRange.y));
            _material.SetFloat(_dissolveHash, 1f);
            StartCoroutine(EffectPlayCoroutine());

        }

        private IEnumerator EffectPlayCoroutine()
        {
            yield return new WaitForSeconds(_dissolveDelay);
            float currentTime = 0;
            while (currentTime < _dissolveDuration)
            {
                currentTime += Time.deltaTime;
                _material.SetFloat(_dissolveHash, Mathf.Lerp(1f, 0f, currentTime / _dissolveDuration));
                yield return null;
            }
        }

        public override void Stop()
        {
            
        }

    }
}