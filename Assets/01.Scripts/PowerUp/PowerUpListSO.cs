using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/PowerUp/List")]
public class PowerUpListSO : ScriptableObject
{
    public List<PowerUpSO> list = new List<PowerUpSO>();
    
}
