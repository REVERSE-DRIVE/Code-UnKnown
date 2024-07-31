using UnityEngine;

namespace ObjectManage
{
    public class ParticleVFXObject : EffectObject
    {
        //[SerializeField] private ParticleSystem[] _particles;

        public void Play()
        {
            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].Play();
            }
        }
    }
}