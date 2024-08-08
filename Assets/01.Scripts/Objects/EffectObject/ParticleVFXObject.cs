using System;
using System.Diagnostics;
using UnityEngine;

namespace ObjectManage
{
    public class ParticleVFXObject : EffectObject
    {
        [SerializeField] protected ParticleSystem[] _particles;

        // 언젠가 이쪽으로 옮겨야함
        public override void Play()
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].Play();
            }
        }

        public override void Stop()
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].Stop();
            }
        }

        public override void ResetItem()
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