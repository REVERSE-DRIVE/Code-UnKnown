using System.Collections;
using UnityEngine;

namespace CombatSkillManage
{
    public class PlayerFragmentSkill : PlayerSkill
    {
        [Header("Attack Setting")]
        [SerializeField] private int _damage;
        [SerializeField] private float _centerRangeRadius;
        [SerializeField] private float _edgeRangeRadius;
        [SerializeField] private float _edgeOffset = 3f;

        [Space(10f)]
        [Header("VFX Setting")]
        [SerializeField] private ParticleSystem _centerVFX;
        [SerializeField] private ParticleSystem _edgeVFX;
        private Transform _edgeVFXTrm;
        private Vector2[] _edgeDirection = {
            new Vector2(1f, 0f),
            new Vector2(0f, 1f),
            new Vector2(-1f, 0f),
            new Vector2(0f, -1f)
        };
        private Collider2D[] _hits = new Collider2D[10];

        private void Awake()
        {
            for (int i = 0; i < _edgeDirection.Length; i++)
            {
                _edgeDirection[i] = _edgeDirection[i] * _edgeOffset;
            }
            _edgeVFXTrm = _edgeVFX.transform;
        }

        public override void UseSkill()
        {
            StartCoroutine(SkillCoroutine());

        }

        private IEnumerator SkillCoroutine()
        {

            _centerVFX.Play();
            yield return new WaitForSeconds(0.7f);
            for (int i = 0; i < _edgeDirection.Length; i++)
            {

                _edgeVFXTrm.position = _edgeDirection[i] + (Vector2)transform.position;
                _edgeVFX.Play();
                DamageToTarget(i);
                Vibration.Vibrate(200);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void DamageToTarget(int index)
        {
            int amount = Physics2D.OverlapCircleNonAlloc(_edgeDirection[index] + (Vector2)transform.position, _edgeRangeRadius, _hits, _targetLayer);
            for (int i = 0; i < amount; i++)
            {
                if (_hits[i].TryGetComponent(out IDamageable hit))
                {
                    hit.TakeDamage(_damage);
                }
            }
        }


    }
}