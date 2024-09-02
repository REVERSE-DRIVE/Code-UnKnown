using UnityEngine;

namespace EnemyManage
{
    public class BossDeadState : BossAVGState
    {
        public BossDeadState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase.MovementCompo.StopImmediately();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (_endTriggerCalled)
            {
                Die();
            }
        }

        private void Die()
        {
            _bossAVGBase.isDead = true;
            _bossAVGBase.ColliderCompo.enabled = false;
            Object.Destroy(_bossAVGBase.gameObject);
        }
    }
}