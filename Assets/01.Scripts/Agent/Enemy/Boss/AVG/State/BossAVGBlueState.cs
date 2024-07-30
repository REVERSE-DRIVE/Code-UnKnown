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
            _bossAVGBase.StartCoroutine(BlueStateRoutine());
        }

        public override void UpdateState()
        {
            base.UpdateState();

        }

        public override void Exit()
        {
            base.Exit();
        }


        private IEnumerator BlueStateRoutine()
        {
            yield return new WaitForSeconds(1f);
            float currentTime = 0;
            float currentCoolingTime = 0;
            int rotationDirection = 1;

            while (currentTime <= _attackTime)
            {
                currentTime += Time.deltaTime;
                _bossAVGBase.transform.rotation =
                    Quaternion.Euler(0, 0,
                        _bossAVGBase.transform.rotation.eulerAngles.z +
                        (rotationDirection) * _bossAVGBase._rotationSpeed);
                currentCoolingTime += Time.deltaTime;
                if (currentCoolingTime > _attackCooltime)
                {
                    currentCoolingTime = 0;
                    Attack();
                }

                if (currentTime >= _attackTime * 0.5f)
                {
                    rotationDirection = -1;
                }

                yield return null;
            }

            _bossAVGBase.StartCoroutine(EndCoroutine());
        }

        private IEnumerator EndCoroutine()
        {
            float currentTime = 0;
            float beforeRotation = _bossAVGBase.transform.rotation.eulerAngles.z;
            while (currentTime <= 0.2f)
            {
                float time = currentTime / 0.2f;
                _bossAVGBase.transform.rotation =
                    Quaternion.Lerp(Quaternion.Euler(0, 0, beforeRotation), Quaternion.identity, time);
                currentTime += Time.deltaTime;
                yield return null;
            }

            _bossAVGBase.transform.rotation = Quaternion.identity;
            _stateMachine.ChangeState(AVGStateEnum.Idle);
        }

        private void Attack()
        {
            Vector2[] directions = VectorCalculator.DirectionsFromCenter(_projectileAmount);
            for (int i = 0; i < _projectileAmount; i++)
            {
                directions[i] = RotateVector2(directions[i], _bossAVGBase.transform.rotation.eulerAngles.z);
                Projectile projectile = PoolingManager.Instance.Pop(_projectile) as Projectile;
                projectile.transform.position = _bossAVGBase.transform.position;
                projectile.Shoot(directions[i]);

            }
        }

        Vector2 RotateVector2(Vector2 vec, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);
            float x = vec.x;
            float y = vec.y;
            vec.x = x * cos - y * sin;
            vec.y = x * sin + y * cos;
            return vec;
        }
    }
}