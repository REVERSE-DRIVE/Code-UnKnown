using UnityEngine;

[CreateAssetMenu(menuName = "SO/Status/AdditionalStat")]
public class AdditionalStat : ScriptableObject
{
    public int bonusExp;

    public int bonusResource;

    public float attackScale = 1;
    //public bool canRevive;
}