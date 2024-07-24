using UnityEngine;

namespace CombatSkillManage
{
    public abstract class PlayerSkill : MonoBehaviour
    {
        public int level;
        protected Player _player;
        
        public void Initialize(Player player)
        {
            _player = player;
        }

        public abstract void UseSkill();
    }
}