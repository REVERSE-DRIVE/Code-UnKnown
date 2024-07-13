using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Items/PowerUp/EffectList")]
public class PowerUpEffectListSO : ScriptableObject
{
    public List<PowerUpEffectSO> list = new List<PowerUpEffectSO>();
    
}
