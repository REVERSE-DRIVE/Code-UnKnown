using System.Collections;
using Calculator;
using ObjectPooling;
using UnityEngine;

namespace EnemyManage
{

    public class BossAVGBlueState : BossAVGState
    {

        private float _attackTime = 10;
        private float _attackCooltime = 0.2f;
        private int _projectileAmount;
        private PoolingType _projectile;
        private int _currentPhaseLevel = 0;

        public BossAVGBlueState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine,
            string animBoolName) : base(
            enemyBase, stateMachine, animBoolName)
        {
        }


        public override void Enter()
        {
            base.Enter();
            _attackTime = _bossAVGBase._attacktime;
            _attackCooltime = _bossAVGBase._attackCooltime;
            _projectileAmount = _bossAVGBase._fireProjectileAmount;
            _projectile = _bossAVGBase._projectile;
            
        }

        public override void UpdateState()
        {
            base.UpdateState();

        }

        public override void Exit()
        {
            base.Exit();
        }

       
    }
}