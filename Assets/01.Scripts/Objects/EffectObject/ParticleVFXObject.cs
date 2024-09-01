using UnityEngine;

namespace ObjectManage
{
    public class ParticleVFXObject : EffectObject
    {
        [SerializeField] protected ParticleSystem[] _particles;

        // 언젠가 이쪽으로 옮겨야함
        public override void Play()
        {
            OnPlayEvent?.Invoke();
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

    }
}