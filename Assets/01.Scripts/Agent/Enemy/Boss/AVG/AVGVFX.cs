using UnityEngine;

public class AVGVFX : AgentVFX
{
    [SerializeField] private ParticleSystem _burstParticle;
    [SerializeField] private ParticleSystem _chargeParticle;
    [SerializeField] private ParticleSystem _yellowAttackImpactVFX;
    [SerializeField] private ParticleSystem _spinAttackVFX;
    [SerializeField] private ParticleSystem _spinAttackStartVFX;
    
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

    public void PlaySpinVFX()
    {
        _spinAttackVFX.Play();
    }

    public void StopSpinVFX()
    {
        _spinAttackVFX.Stop();
    }

    public void PlaySpinStartVFX()
    {
        _spinAttackStartVFX.Play();
    }
}
