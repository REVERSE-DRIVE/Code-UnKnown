using UnityEngine;
using DG.Tweening;


namespace CombatSkillManage
{
    public class PlayerPinderSkill : PlayerSkill
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _castCircleRadius;
        [SerializeField] private float _range;
        //"플레이어의 스킬이 '파인더'로 변경됨
        //-> 파인더: 스킬 시전 시, 플레이어가 보는 방향 일직선으로 5 * 10 크기의 레이저를 발사하여 레이저 범위 내에 들어온 적들에게 30의 피해를 입힘 (플레이어 공격 이팩트 레이저 효과 활용)"   
        [Header("VFX Setting")]
        [SerializeField] private Transform _laserTrm;
        [SerializeField] private ParticleSystem[] _vfxs;
        private RaycastHit2D[] _hits = new RaycastHit2D[10];
        private FeedbackPlayer _soundFeedbackPlayer; 

        private void Awake()
        {
            _soundFeedbackPlayer = GetComponentInChildren<FeedbackPlayer>();
        }


        public override void UseSkill()
        {
            // 레이저 임팩트
            _laserTrm.DOScaleY(2f, 0.1f).OnComplete(() => _laserTrm.DOScaleY(0f, 0.15f));
            _laserTrm.right = _player.PlayerInputCompo.Direction;
            DamageToTarget();
            for (int i = 0; i < _vfxs.Length; i++)
            {
                _vfxs[i].Play();
            }
            CameraManager.Instance.Shake(30f, 0.2f);
            Vibration.Vibrate(200); // 진동

            _soundFeedbackPlayer.PlayFeedback();

        }
   
        private void DamageToTarget() // 타겟 감지후 데미지 적용
        {
            int amount = Physics2D.CircleCastNonAlloc(_player.transform.position, _castCircleRadius, _player.PlayerInputCompo.Direction, _hits, _range, _targetLayer);
            for (int i = 0; i < amount; i++)
            {
                if(_hits[i].collider.transform.TryGetComponent(out IDamageable hit)){
                    hit.TakeDamage(_damage);
                }
            }
        }


    }
}