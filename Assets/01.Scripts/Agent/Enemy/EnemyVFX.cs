using UnityEngine;

public class EnemyVFX : AgentVFX
{
    [SerializeField] private ParticleSystem[] _effects;
    public void PlayEffect()
    {
        foreach (var effect in _effects)
        {
            effect.Play();
        }
    }
}
