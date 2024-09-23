using DG.Tweening;
using ObjectPooling;
using TMPro;
using UnityEngine;

public class ErrorPanelObject : MonoBehaviour, IPoolable
{
    [SerializeField] private int _showPercent = 50;
    private Transform _visualTrm;
    private TextMeshPro _text;
    private SpriteRenderer _alertRenderer;
    [field:SerializeField]public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;


    private void Awake()
    {
        _visualTrm = transform.Find("Visual");
        _text = GetComponentInChildren<TextMeshPro>();
        _alertRenderer = transform.Find("Alert").GetComponent<SpriteRenderer>();

    }

    public void Initialize(Vector2 position)
    {
        transform.position = position;
        if (Random.Range(0, 100) < _showPercent)
        {
            _text.enabled = true;
            _text.text = $"{ComputerManager.Instance.InfectionLevel}%";
            _alertRenderer.enabled = false;
        }
        else
        {
            _text.enabled = false;
            _alertRenderer.enabled = true;
        }
        
        _visualTrm.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(_visualTrm.DOScaleX(1f, 0.2f));
        seq.Append(_visualTrm.DOScaleY(1f, 0.2f));

    }

    public void SetInfectText(int level)
    {
        _text.text = $"{level}%";

    }

    public void ResetItem()
    {
    }
}