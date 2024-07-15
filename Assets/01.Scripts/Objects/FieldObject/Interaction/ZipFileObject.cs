using System;
using System.Collections;
using DG.Tweening;
using ItemManage;
using ObjectManage;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZipFileObject : InteractObject
{
    [SerializeField] private ItemType _dropType;
    [SerializeField] private int _minDropAmount;
    [SerializeField] private int _maxDropAmount;
    [SerializeField] private ParticleSystem _openParticle;
    [SerializeField] private Material _dissolveMaterial;
    
    private Transform _visualTransform;
    protected Animator _animator;
    protected SpriteRenderer _visualRenderer;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Awake()
    {
        _visualTransform = transform.Find("Visual");
        _animator = _visualTransform.GetComponent<Animator>();
        _visualRenderer = _visualTransform.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        OnInteractEvent += HandleInteract;
    }

    private void OnDisable()
    {
        OnInteractEvent -= HandleInteract;
    }

    protected virtual void HandleInteract()
    {
        
        Instantiate(_openParticle, transform.position, Quaternion.identity);
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        _animator.SetTrigger(Open);
        yield return new WaitForSeconds(1.5f);
        _visualRenderer.material = _dissolveMaterial;
        _dissolveMaterial.DOFloat(0f, "_DissolveLevel", 0.5f);
        yield return new WaitForSeconds(0.5f);
        int dropAmount = Random.Range(_minDropAmount, _maxDropAmount);
        Vector3 position = transform.position;
        for (int i = 0; i < dropAmount; i++)
        {
            position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            ItemDropManager.Instance.DropItem(_dropType, 0, position);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
