using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/BossInfo/Info")]
public class BossInfoSO : ScriptableObject
{
    public int id;
    public string bossName;
    public Sprite bossIcon;
    public Color personalColor;
    public Boss bossPrefab;
}