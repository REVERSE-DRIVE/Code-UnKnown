using UnityEngine;

namespace EnemyManage
{
    public class BossAVGStunState : BossAVGState
    {
        private float _stateDuration = 10f;
        private float _currentTime = 0;
        public BossAVGStunState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase.Stat.isResist = false;
            _bossAVGBase.CanStateChangeable = false;
            _currentTime = 0;
            _stateDuration = _bossAVGBase._stunDuration;
            Debug.Log("스턴 진입");
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentTime += Time.deltaTime;
            if (_currentTime >= _stateDuration)
            {
                Debug.Log("스턴 쿨타임 종료");
                _currentTime = 0;
                _stateMachine.ChangeState(AVGStateEnum.Idle, true);
        
            }
        }


        public override void Exit()
        {
            base.Exit();
            _bossAVGBase.Stat.isResist = true;
            _bossAVGBase.CanStateChangeable = true;
            _bossAVGBase.ResetPosition();
        }
    }
}