using UnityEngine;

namespace EnemyManage
{
    public class BossAVGGreenState : BossAVGState
    {
        private float _currentTime = 0;
        private float _currentStateTime = 0;
        private float _stateDuration;
        private int _currentHealth;
        private int _currentCoreDestroyCount;
        
        public BossAVGGreenState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }
        

        public override void Enter()
        {
            base.Enter();
            _currentTime = 0;
            _currentStateTime = 0;
            _currentCoreDestroyCount = 0;
            _stateDuration = _bossAVGBase._greenStateDuration;

            _currentHealth = _bossAVGBase.HealthCompo.CurrentHealth;
            OnAllHealingCore();
        }

        private void OnAllHealingCore()
        {
            for (int i = 0; i < _bossAVGBase._healingObjects.Length; i++)
            {
                _bossAVGBase._healingObjects[i].OnCore(_bossAVGBase);
            }
        }

        private void OffAllHealingCore()
        {
            for (int i = 0; i < _bossAVGBase._healingObjects.Length; i++)
            {
                _bossAVGBase._healingObjects[i].Destroy();

            }
        }

        public override void UpdateState()
        {
            float time = Time.deltaTime;
            _currentTime += time;
            _currentStateTime += time;
            if (_currentTime >= 1)
            {
                _currentTime = 0;
                HealWithCore();
            }

            if (_currentStateTime >= _stateDuration)
            {
                _stateMachine.ChangeState(AVGStateEnum.Idle);
            }
        }

        private void HealWithCore()
        {
            int totalHeal = 0;
            for (int i = 0; i < _bossAVGBase._healingObjects.Length; i++)
            {
                if (_bossAVGBase._healingObjects[i].isActive)
                {
                    totalHeal += _bossAVGBase._healCoreHealAmountPerSecond;
                } 
            }
            _bossAVGBase.HealthCompo.RestoreHealth(totalHeal);
        }


        
        public override void Exit()
        {
            base.Exit();
            OffAllHealingCore();
            //_bossAVGBase.OffHealDefense();
        }

        private void HandleHealDefence()
        {
            
        }

        public override void CustomTrigger()
        {
            base.CustomTrigger();
            _currentCoreDestroyCount++;
            
            // 이 위까지는 이상무
            if (_currentCoreDestroyCount >= 4)
            {
                _currentCoreDestroyCount = 0;
                Debug.Log("여기서 스턴이 먹어야되는데???");
                _currentStateTime = 0;
                _stateMachine.ChangeState(AVGStateEnum.Stun, true);
            }
        }
    }
}