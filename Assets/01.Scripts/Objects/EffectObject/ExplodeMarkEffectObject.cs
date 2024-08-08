using System;
using System.Collections;
using System.Collections.Concurrent;
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
        [SerializeField] private float _dissolveDuration = 1f;
        [SerializeField] private Vector2 _noiseRandomRange;

        private void Awake()
        {
            _dissolveHash = Shader.PropertyToID("_");
            _visualTrm = transform.Find("Visual");
            _visualTrm.GetComponent<SpriteRenderer>();
        }


        public override void Play()
        {
            StartCoroutine(EffectPlayCoroutine());

        }

        private IEnumerator EffectPlayCoroutine()
        {
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

        public override void ResetItem()
        {
        }
    }
}