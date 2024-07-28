using UnityEngine;

namespace CombatSkillManage
{
    [CreateAssetMenu(menuName = "SO/PlayerSkill")]
    public class PlayerSkillSO : ScriptableObject
    {
        public int id;
        public string skillName;
        public Sprite skillIcon;
        public string description;
        public float coolTime;
        
        public PlayerSkill skillPrefab;

    }
}