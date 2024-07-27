using UnityEngine;

public class SpinProjectile: Projectile
{
    [SerializeField] private float _spinSpeed;
    private float _currentSpinTime = 0;
    
    protected override bool Update()
    {
        if (!base.Update()) return false;
        _currentSpinTime += Time.deltaTime * _spinSpeed;
        
        _visualTrm.rotation = Quaternion.Euler(0,0,_currentSpinTime);
        return true;
    }
}