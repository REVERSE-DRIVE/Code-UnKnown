using System.Collections;
using DG.Tweening;
using ObjectPooling;
using TMPro;
using UnityEngine;

public struct TextContent
{
    public string content;
    public int size;
    public Color color;
    public float lifeTime;
}

public class TextEffectObject : MonoBehaviour, IPoolable
{
    [field:SerializeField] public PoolingType type { get; set; }
    public GameObject ObjectPrefab => gameObject;

    [Header("TextEffect Setting")] 
    [SerializeField] private float _yDelta;
    
    [Header("Content Setting")]
    [SerializeField] private TextMeshPro _tmp;
    [SerializeField] private float _lifeTime;
    private bool _isActive;

    public void Initialize(TextContent content, Vector2 position)
    {
        transform.position = position;
        
        _tmp.text = content.content;
        _tmp.color = content.color;
        _tmp.fontSize = content.size;
        _lifeTime = content.lifeTime;
        transform.localScale = Vector2.one;
    }

    public void Play()
    {
        _isActive = true;
        transform.DOScale(Vector2.zero, _lifeTime).SetEase(Ease.InExpo);
        transform.DOMoveY(transform.position.y+_yDelta, _lifeTime).OnComplete(()=> HandleDie());
    }

    private void HandleDie()
    {
        PoolingManager.Instance.Push(this);
    }

    public void ResetItem()
    {
        
    }
}