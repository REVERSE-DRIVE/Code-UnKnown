using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSkillManage
{
    public class PlayerMultiShootSkill : PlayerSkill
    {
        [SerializeField] private Transform _targetMark;
        [SerializeField] private List<Transform> _targetMarkList;

        private Collider2D _targets;
        
        public override void UseSkill()
        {
            
            
        }

        private IEnumerator SkillCoroutine()
        {

            yield return new WaitForSeconds(0.1f);
            
        }
        
        
    }
}