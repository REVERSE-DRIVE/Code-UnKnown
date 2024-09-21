using DG.Tweening;
using UnityEngine;

public class BackDoorVisual : MonoBehaviour
{
    [SerializeField] private float _activeDuration = 1f;
    private ParticleSystem _backDoorVFX;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = transform.parent.GetComponent<Collider2D>();
        _backDoorVFX = GetComponentInChildren<ParticleSystem>();
        _collider.enabled = false;
        transform.localScale = Vector3.zero;
    }

    public void Active()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, _activeDuration).OnComplete(() => _collider.enabled = true);
        _backDoorVFX.Play();
    } 


}
