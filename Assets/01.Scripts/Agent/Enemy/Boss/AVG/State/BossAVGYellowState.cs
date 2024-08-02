using UnityEngine;

namespace EnemyManage
{
    public class BossAVGYellowState : BossAVGState
    {
        private float _stateDuration;
        private float _currentTime = 0;
        
        public BossAVGYellowState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase._isResist = true;
            _stateDuration = _bossAVGBase._yellowStateDuration;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            // _currentTime += Time.deltaTime;
            // if (_currentTime >= _stateDuration)
            // {
            //     _stateMachine.ChangeState(AVGStateEnum.Idle);
            // }
        }

        internal void BreakState()
        {
            _bossAVGBase._isResist = false;
            _bossAVGBase.ForceStun();
            
        }

        public void CustomTrigger()
        {
            BreakState();

        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}