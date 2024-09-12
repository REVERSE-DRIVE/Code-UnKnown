using System.Collections;
using ObjectPooling;
using UnityEngine;

public class ScratchEffectObject : MonoBehaviour, IPoolable
{
    [SerializeField] private Sprite[] _scratchList;
    [SerializeField] private float _duration;
    private SpriteRenderer _visualRenderer;
    private Material _material;
    private int _powerHash;
    private Transform _visualTrm;
    private float[] _randomAngleList = {0f, 90f, 180f, 270f};

    [field:SerializeField] public PoolingType type {get; set;}

    public GameObject ObjectPrefab => gameObject;
    private void Awake()
    {
        _visualTrm  = transform.Find("Visual");
        _powerHash = Shader.PropertyToID("_Power");
        _visualRenderer = _visualTrm.GetComponent<SpriteRenderer>();
        _material = _visualRenderer.material;
    }


    public void SetPostion(Vector2 position){
        transform.position = position;
    }

    public void Initialize()
    {
        _visualRenderer.sprite = _scratchList[Random.Range(0, _scratchList.Length)];    
        _visualTrm.rotation = Quaternion.Euler(0,0,Random.Range(0, 360f));
        StartCoroutine(VisualCoroutine());
    }

    private IEnumerator VisualCoroutine()
    {
        float currentTime = 0f;
        float duration = Random.Range(_duration, _duration+5f);
        while (currentTime < duration){
            currentTime += Time.deltaTime;
            _material.SetFloat(_powerHash, Mathf.Lerp(1f, 0f, currentTime / duration));
            yield return null;
        }
        _material.SetFloat(_powerHash, 0f);
    }

    public void ResetItem()
    {
        Initialize();
    }
}
