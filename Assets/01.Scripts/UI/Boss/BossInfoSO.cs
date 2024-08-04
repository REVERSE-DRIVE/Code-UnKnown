using UnityEngine;
using UnityEngine.Serialization;

public class BossInfoSO : ScriptableObject
{
    public string bossName;
    public Sprite bossIcon;
    public Color personalColor;
    public Boss bossPrefab;
}