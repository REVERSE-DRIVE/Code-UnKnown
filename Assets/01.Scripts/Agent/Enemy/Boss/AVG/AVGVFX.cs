using UnityEngine;

public class AVGVFX : AgentVFX
{
    [SerializeField] private ParticleSystem _burstParticle;
    [SerializeField] private ParticleSystem _chargeParticle;
    [SerializeField] private ParticleSystem _yellowAttackImpactVFX;
    public void PlayBurst()
    {
        _burstParticle.Play();
    }

    public void PlayCharge()
    {
        _chargeParticle.Play();
    }

    public void StopCharge()
    {
        _chargeParticle.Stop();
    }

    public void PlayYellowImpact()
    {
        _yellowAttackImpactVFX.Play();
    }
}
