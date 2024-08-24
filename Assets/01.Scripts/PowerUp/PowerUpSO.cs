using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Items/PowerUp/Data")]
public class PowerUpSO : ScriptableObject
{
    public int id;
    public string code;
    public Color powerUpColor;
    public int needLevel;
    public int maxCollect;
    public PowerUpRank powerUpRank;
    public PlayerSkill shouldBeUnlock;
    public string title;
    [TextArea] public string description;
    public Sprite icon;

    public List<PowerUpEffectSO> effectList;

    public bool CheckCanUpgrade()
    {
        if (shouldBeUnlock != PlayerSkill.None)
        {
            Skill skill = SkillManager.Instance.GetSkill(shouldBeUnlock);
            if (skill.skillEnabled == false) return false;
        }
        if (PowerUpManager.Instance.Find(id) >= maxCollect)
            return false;

        if (LevelManager.Instance.CurrentLevel < needLevel) return false;

        if (effectList.Any(e => e.CanUpgradeEffect() == false)) 
            return false;

        return true;
    }

}