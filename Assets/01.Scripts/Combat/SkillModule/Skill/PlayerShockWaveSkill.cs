using System.Collections;
using UnityEngine;


namespace CombatSkillManage
{

    public class PlayerShockWaveSkill : PlayerSkill
    {
        [SerializeField] private Vector2 _detectBoxSize;
        [SerializeField] private int _damage;
        private Transform _rangeVisualTrm;
        private Collider2D[] _hits = new Collider2D[10];
        [SerializeField] private ParticleSystem[] _vfxs;
        private FeedbackPlayer _impactSoundFeedback;

        private void Awake()
        {
            _rangeVisualTrm = transform.Find("RangeVisual");
            _impactSoundFeedback = transform.Find("ImpactSoundFeedback").GetComponent<FeedbackPlayer>();
        }

        public override void UseSkill()
        {
            StartCoroutine(SkillCoroutine());
            
        }

        private IEnumerator SkillCoroutine()
        {
            _rangeVisualTrm.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            _rangeVisualTrm.gameObject.SetActive(false);
            for (int i = 0; i < _vfxs.Length; i++)
            {
                _vfxs[i].Play();
            }
            CameraManager.Instance.SetShake(20f, 40f);
            for (int i = 0; i < 3; i++)
            {
                Vibration.Vibrate(100);
                DamageToTarget();
                _impactSoundFeedback.PlayFeedback();
                yield return new WaitForSeconds(0.15f);
            }
            CameraManager.Instance.ShakeOff();
        }

        private void DamageToTarget()
        {
            int amount = Physics2D.OverlapBoxNonAlloc(transform.position, _detectBoxSize, 0, _hits, _targetLayer);
            for (int i = 0; i < amount; i++)
            {
                if (_hits[i].transform.TryGetComponent(out IDamageable hit))
                {
                    hit.TakeDamage(_damage);

                }
            }
        }
    }

}