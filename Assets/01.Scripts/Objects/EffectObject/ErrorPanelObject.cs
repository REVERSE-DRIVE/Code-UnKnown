using System;
using System.Collections;
using DG.Tweening;
using ObjectPooling;
using TMPro;
using UnityEngine;

public class ErrorPanelObject : MonoBehaviour, IPoolable
{
    private Transform _visualTrm;
    private TextMeshPro _text;
    [field:SerializeField]public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;


    private void Awake()
    {
        _visualTrm = transform.Find("Visual");
        _text = GetComponentInChildren<TextMeshPro>();
    }

    public void Initialize(Vector2 position)
    {
        transform.position = position;
        _text.text = $"{ComputerManager.Instance.InfectionLevel}%";
        _visualTrm.localScale = Vector3.zero;
        Sequence seq = DOTween.Sequence();
        seq.Append(_visualTrm.DOScaleX(1f, 0.2f));
        seq.Append(_visualTrm.DOScaleY(1f, 0.2f));

    }

    public void ResetItem()
    {
    }
}