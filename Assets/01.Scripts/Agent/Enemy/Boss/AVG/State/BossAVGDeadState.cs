using QuestManage;
using UnityEngine;

namespace EnemyManage
{
    public class BossAVGDeadState : BossAVGState
    {
        public BossAVGDeadState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            _bossAVGBase.CanStateChangeable = false;
            QuestObserver.Instance.Trigger(QuestType.Kill, 1, EnemyType.Boss);
            _bossAVGBase.isDead = true;
            _bossAVGBase.ColliderCompo.enabled = false;
            Object.Destroy(_bossAVGBase.gameObject);
        }
    }
}