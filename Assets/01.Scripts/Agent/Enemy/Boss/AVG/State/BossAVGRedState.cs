using System.Collections;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace EnemyManage
{
    public class BossAVGRedState : BossAVGState
    {
        private int _chargeEnergyAmount = 10;
        private float _chargingLevel = 0;
        private float _chargingSpeed = 1;
        private bool isChargeOver;
        private bool isPlayedSound;
        private CameraManager _camManagerCashing;
        
        public BossAVGRedState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _camManagerCashing = CameraManager.Instance;
            _bossAVGBase._structureObject.Active(_bossAVGBase.transform);
            _bossAVGBase.AVGVFXCompo.PlayCharge();
            SetDefault();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
            Charge();
        }


        public override void Exit()
        {
            base.Exit();
        }

        private void SetDefault()
        {
            _chargingLevel = 0;
            _chargeEnergyAmount = _bossAVGBase._chargeEnergy;
            _chargingSpeed = _bossAVGBase._chargingSpeed;
            isChargeOver = false;
            isPlayedSound = false;
        }

        private void Charge()
        {
            if (isChargeOver) return;
            
            _chargingLevel += Time.deltaTime * _chargingSpeed;
            _camManagerCashing.SetShake(_chargingLevel * 0.5f, 5);
            if (!isPlayedSound && _chargingLevel > _chargeEnergyAmount - 3.5f)
            {
                //_bossAVGBase._soundObject.PlayAudio(4);
                isPlayedSound = true;

            }
            if ( _chargingLevel > _chargeEnergyAmount - 3)
            {
                _bossAVGBase.AVGVFXCompo.StopCharge();
            }
            if (_chargingLevel >= _chargeEnergyAmount)
            {
                isChargeOver = true;
                _chargingLevel = 0;
                BurstAttack();
            }
        }

        private void BurstAttack()
        {
            _stateMachine.ChangeState(AVGStateEnum.Idle);
            _camManagerCashing.ShakeOff();
            _camManagerCashing.Shake(40f, 1);
            
            _bossAVGBase.StartCoroutine(BurstOverCoroutine());
            
        }

        private IEnumerator BurstOverCoroutine()
        {
            _bossAVGBase._structureObject.OffObject();

            yield return new WaitForSeconds(0.2f);
            _bossAVGBase.AVGVFXCompo.PlayBurst();

            _bossAVGBase._structureObject.DefenseAVGBurst(_bossAVGBase._burstDamage);

        }
    }
}